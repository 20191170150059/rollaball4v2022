using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlle : MonoBehaviour
{
    public float moveSpeed;
    public float maxVelocity;

    public float rayDistance;
    public LayerMask groundLayer;
    
    private Gameicontrole _gameicontrole;
    private PlayerInput _playerInput;
    private Camera _mainCamara;
    private Rigidbody _rigidbody;

    private Vector2 _moveInput;

    private bool _isGrounded;

    private void OnEnable()
    {
        //inicializacao de variavel 
        _gameicontrole = new Gameicontrole();

        //referencia dos componentes no mesmo objeto do unity
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();

        //referencia para a camera main guardada no closse Camera
        _mainCamara = Camera.main;

        //atribuindo ao delegote do Action Triggered no player input 
        _playerInput.onActionTriggered += OnActionTriggered;
    }

    private void OnDisable()
    {
        //atribuindo ao delegote do Action Triggered no player input 
        _playerInput.onActionTriggered -= OnActionTriggered;
    }

    private void OnActionTriggered(InputAction.CallbackContext obj)
    {
        //comparando o nome Action que esta chegando
        
            
        if (obj.action.name.CompareTo(_gameicontrole.GamePlay.jump.name) == 0)
        {
            _moveInput = obj.ReadValue<Vector2>();
        }

        if (obj.action.name.CompareTo(_gameicontrole.GamePlay.jump.name) == 0)
        {
            if (obj.performed)
            {
                
            }
        }
    }
    private void Mover()
    {
        Vector3 camForward = _mainCamara.transform.forward;
        camForward.y = 0;

        //calcular o movimento no eixo da camera para o movimento frente/tras
        Vector3 moveVertical = _mainCamara.transform.forward * _moveInput.y;

        //pegar o vetor q aponta para o lado direito da camera zeremos o componente y
        Vector3 camRight = _mainCamara.transform.right;
        camRight.y = 0;

        //calcular o movimento do eixo da camera para o movimento esquerdo/direito
        Vector3 moveHorizontal = _mainCamara.transform.right * _moveInput.x;

        //adicionar a for??a no eixo no objeto atraves do rididbody, com intensidade definida por moveSpeed
        _rigidbody.AddForce((moveVertical + moveHorizontal) * moveSpeed * Time.fixedDeltaTime);

    }



    private void FixedUpdate()
    {
        Mover();
    }

    private void LimitVelocity()
    {
        //pegar a velocidade do player 
        Vector3 velocity = _rigidbody.velocity;

        //checar se a velocidaded esta dentro dos limites nos diferentes eixos

        //limitando o eixo x usamdo ifs, abs e sing 
        if (Mathf.Abs(velocity.x) > maxVelocity) velocity.x = Mathf.Sign(velocity.x) * maxVelocity;

        //-maxvelocity < velocity.2 < maxVelocity
        velocity.z = Mathf.Clamp( velocity.z, min:-maxVelocity, maxVelocity);
        //alterar a velocidade do player
    }
    
    /* como fazer o jogador pular:
     * 1 -Checar se o jogador est?? no ch??o
     * -- a - Checar colis??o a partir da f??sica (usando os eventos de colis??o)
     * -- a - vantagem: facil de implementar (adicionar uma fun????o que ja existe no unity - OnCollisionenter)
     * -- a - desvantagem: n??o sabemos a hora exata que o unity vai chamar essa fun????o (pode ser que o jogador
     * toque no ch??o e demore alguns frames pra o jogo saber que ele est?? no ch??o)
     * -- b - atraves do raycast: o---| bolinha vai atirar um raio, o raio vai bater em algum objeto e a gente
     * recebe o resultado dessa colis??o.
     * -- b -podemos usar Layers pra definir quais objetos que o raycast deve checar colis??o
     * -- b - vantagem: Resposta da colis??o ?? imediata
     * -- b - desvantagem: Um pouco mais trabalhoso de configurar
     * -- Uma variavel bool que vai dizer para o resto do codigo se o jogador estar?? no ch??o (true) ou n??o (false)
     * 2 - Jogador precisa apertar o bot??o de pulo
     *  -- precismaos configurar o bot??o a ser utilizado para a????o de pular no nosso Input
     *  -- na fun????o OnActionTriggered precisaremos comparar se a a????o recebida tem o mesmo nome da a????o de pulo
     *  -- Precisamos dizer em qual momento do bot??o ser apertado queremos executar o pulo (started, canceled, perform
     * 3 - Realizar o pulo atrav??s da f??sica
     * -- Vamos criar uma fun????o que vai realizar o pulo
     * -- Se o personagem estiver no ch??o, iremos aplicar uma for??a para cima com uma certa magnitude
     */

    private void Jump()
    {
        
        
    }
    private void CheckGround()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, rayDistance, (int) groundLayer);
    }

    private void Update()
    {
        CheckGround();
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.yellow);
    }
}
