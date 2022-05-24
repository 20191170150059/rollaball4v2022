using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlle : MonoBehaviour
{
    public float moveSpeed;
    
    private Gameicontrole _gameicontrole;
    private PlayerInput _playerInput;
    private Camera _mainCamara;
    private Rigidbody _rigidbody;

    private Vector2 _moveInput;

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
        if (obj.action.name.CompareTo(_gameicontrole.GamePlay.Movement.name) == 0)

            _moveInput = obj.ReadValue<Vector2>();
    }

    private void Mover()
                {
                    //calcular o movimento no eixo da camera para o movimento frente/tras
                    Vector3 moveVertical = _mainCamara.transform.forward * _moveInput.y;
                    
                    //calcular o movimento do eixo da camera para o movimento esquerdo/direito
                    Vector3 moveHorizontal = _mainCamara.transform.right * _moveInput.x;
                        
                    //adicionar a força no eixo no objeto atraves do rididbody, com intensidade definida por moveSpeed
                    _rigidbody.AddForce((moveVertical + moveHorizontal)* moveSpeed * Time.fixedDeltaTime);
                    
                }
            
        

    private void FixedUpdate()
    {
        Mover();
    }
}
