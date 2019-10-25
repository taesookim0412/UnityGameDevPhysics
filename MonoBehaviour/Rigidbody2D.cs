public float speed;
private Rigidbody2D rb;
private Vector2 moveVelocity;
// Start is called before the first frame update
void Start()
{
    rb = GetComponent<Rigidbody2D>();
}

void Update()
{
    Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    moveVelocity = moveInput.normalized * speed;
}

void Update(){
    transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
}

void FixedUpdate(){
    rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime, ForceMode.Impulse);
}