using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Velocidade do Jogador
	public float speed = 6f;

	//Vetor responsavel pelo movimento
	Vector3 movement; 

	//Responsavel pela transicao da animacao
	Animator anim;

	//Responsavel pela fisica do Objeto
	Rigidbody playerRigidbody;

	//Mascara de Chao
	int floorMask;

	//Informacao de RayCast
	float camRayLenght = 100f;

	void Awake(){
		//Pegando o layer do Chao
		floorMask = LayerMask.GetMask("Floor");

		//Atribuicoes e Referencias

		//pegando animacao
		anim = GetComponent<Animator>();

		//Corpo do player
		playerRigidbody = GetComponent<Rigidbody> ();

	}

	void FixedUpdate(){

		//Pegando parametros do inout na Horizontal
		float h = Input.GetAxisRaw ("Horizontal");
		//Pegando parametros do input na vertical
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);
		Turning ();
		Animating (h,v);
	}

	//Movimentando o player
	void Move( float h, float v) {

		//determina o movimento
		movement.Set (h, 0f, v);

		//Normaliza o movimento
		movement = movement.normalized * speed * Time.deltaTime;

		//Setando o movimento no personagem
		playerRigidbody.MovePosition (transform.position + movement);
	}

	//Rotacionando o Player
	void Turning(){

		//Pegando o input do mouse
		Ray camRay = Camera.main.ScreenPointToRay( Input.mousePosition );

		RaycastHit floorHint;

		//verificando o raio da camera 
		if (Physics.Raycast (camRay, out floorHint, camRayLenght, floorMask)) {

			//Normalizando o mouse
			Vector3 playerToMouse = floorHint.point - transform.position;

			//Fixando ovalor para o player
			playerToMouse.y = 0f;

			//Criando uma Rotacao
			Quaternion newRotation = Quaternion.LookRotation( playerToMouse );

			//Setando a rotacao para o player
			playerRigidbody.MoveRotation( newRotation );
		}
		
	}

	void Animating( float h, float v) {
		//vendo se esta em movimento
		bool walking =  h != 0f || v != 0f ;
		//setando o parametro
		anim.SetBool("isWalking", walking);
	}

}
