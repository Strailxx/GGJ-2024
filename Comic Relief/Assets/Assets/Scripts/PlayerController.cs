using System;
using UnityEngine;

namespace PlayerController
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] private ScriptableStats _stats;
        private Rigidbody2D _rb;
        private CapsuleCollider2D _col;
        private FrameInput _frameInput;
        private Vector2 _frameVelocity;
        private bool _cachedQueryStartInColliders;

        private bool _insideComic;
        private GameObject _collidersRoot;

        public Animator animator;

        #region Interface

        public Vector2 FrameInput => _frameInput.Move;
        public event Action<bool, float> GroundedChanged;
        public event Action Jumped;

        #endregion

        private float _time;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<CapsuleCollider2D>();

            _insideComic = true;

            _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
        }
        private void Start()
        {
            _collidersRoot = GameObject.Find("/Colliders");
        }
        private void Update()
        {
            _time += Time.deltaTime;
            GatherInput();
        }
        private void GatherInput()
        {
            _frameInput = new FrameInput
            {
                JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
                JumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C),
                Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")),
                // TODO this should use a mappable button
                ScissorsKeyDown = Input.GetKeyDown(KeyCode.Q)
            };

            if (_frameInput.JumpDown)
            {
                _jumpToConsume = true;
                _timeJumpWasPressed = _time;
            }
        }
        private void FixedUpdate()
        {
            CheckCollisions();
            // Handle scissors before applying physics as it may change whether the player is in the comic.
            HandleScissors();
            HandleJump();
            HandleDirection();
            HandleGravity();
            ApplyMovement();
        }
        
        private float _frameLeftGrounded = float.MinValue;
        private bool _grounded;
        private Collider2D _touchingScissorableCollider;

        private void CheckCollisions()
        {
            Physics2D.queriesStartInColliders = false;

            // Ground and Ceiling
            bool groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, _stats.GrounderDistance, ~_stats.PlayerLayer);
            bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, _stats.GrounderDistance, ~_stats.PlayerLayer);

            // Hit a Ceiling
            if (ceilingHit) _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);

            // Landed on the Ground
            if (!_grounded && groundHit)
            {
                animator.SetBool("isJumping", false);
                _grounded = true;
                _coyoteUsable = true;
                _bufferedJumpUsable = true;
                _endedJumpEarly = false;
                GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y));
            }
            // Left the Ground
            else if (_grounded && !groundHit)
            {
                animator.SetBool("isJumping", true);
                _grounded = false;
                _frameLeftGrounded = _time;
                GroundedChanged?.Invoke(false, 0);
            }

            // Near a wall that can be scissored
            _touchingScissorableCollider = null;
            Collider2D[] colliders = _collidersRoot.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D collider in colliders)
            {
                string colliderName = collider.transform.gameObject.name;
                // Inside of the comic we can scissor out through sides. Outside we can scissor in anywhere.
                bool colliderScissorable = colliderName == "Left" ||
                                           colliderName == "Right" ||
                                           (!_insideComic && (
                                                colliderName == "Top" ||
                                                colliderName == "Bottom"));
                if (!colliderScissorable) continue; // Not a scissorable collider.

                // Check if player is close enough to the collider
                Vector2 closestPoint = collider.ClosestPoint(_rb.position);
                float distance = Vector2.Distance(closestPoint, _rb.position);
                // TODO pull scissorable distance out to ScriptableStats
                if (distance > 0.5) continue; // Scissorable collider too far.

                _touchingScissorableCollider = collider;
            }

            Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
        }

        // TODO Player currently always has scissors. Make the player pick up scissors before being
        // able to use them.
        private bool _holdingScissors = true;
        private void HandleScissors()
        {
            if (!_holdingScissors) return;

            if (_frameInput.ScissorsKeyDown && _touchingScissorableCollider != null) {
                // Cut player through wall
                Vector2 offset = _rb.position - _touchingScissorableCollider.ClosestPoint(_rb.position);
                _rb.position -= 2*offset;
                _insideComic = !_insideComic;
            }
        }

        private bool _jumpToConsume;
        private bool _bufferedJumpUsable;
        private bool _endedJumpEarly;
        private bool _coyoteUsable;
        private float _timeJumpWasPressed;

        private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer;
        private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _stats.CoyoteTime;

        private void HandleJump()
        {
            // No jumping when outside of the comic (no gravity)
            if (!_insideComic) return;

            if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;

            if (!_jumpToConsume && !HasBufferedJump) return;

            if (_grounded || CanUseCoyote) ExecuteJump();

            _jumpToConsume = false;
        }

        private void ExecuteJump()
        {
            animator.SetBool("jump", true);
            _endedJumpEarly = false;
            _timeJumpWasPressed = 0;
            _bufferedJumpUsable = false;
            _coyoteUsable = false;
            _frameVelocity.y = _stats.JumpPower;
            Jumped?.Invoke();
            Invoke("JumpDelay", 0.2f);
        }

        private void HandleDirection()
        {
            var isRunning = false;
            if (_frameInput.Move.x == 0)
            {
                var deceleration = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else
            {
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * _stats.MaxSpeed, _stats.Acceleration * Time.fixedDeltaTime);
                if (_frameVelocity.x > 0) {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                } else if (_frameVelocity.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                isRunning = true;
            }

            // Outside of comic we can also move in the y direction with input.
            if (!_insideComic)
            {
                if (_frameInput.Move.y == 0)
                {
                    var deceleration = _stats.AirDeceleration;
                    _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, 0, deceleration * Time.fixedDeltaTime);
                }
                else
                {
                    _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, _frameInput.Move.y * _stats.MaxSpeed, _stats.Acceleration * Time.fixedDeltaTime);
                    isRunning = true;
                }
            }
            
            animator.SetBool("isRunning", isRunning);
        }

        private void HandleGravity()
        {
            // No gravity outside of the comic.
            if (!_insideComic) return;

            if (_grounded && _frameVelocity.y <= 0f)
            {
                _frameVelocity.y = _stats.GroundingForce;
            }
            else
            {
                var inAirGravity = _stats.FallAcceleration;
                if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _stats.JumpEndEarlyGravityModifier;
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
            }
        }

        private void ApplyMovement() => _rb.velocity = _frameVelocity;

        public void JumpDelay()
        {
            animator.SetBool("jump", false);
        }
    }

    public struct FrameInput
    {
        public bool JumpDown;
        public bool JumpHeld;
        public Vector2 Move;
        public bool ScissorsKeyDown;
    }


    public interface IPlayerController
    {
        public event Action<bool, float> GroundedChanged;

        public event Action Jumped;
        public Vector2 FrameInput { get; }
    }
}
