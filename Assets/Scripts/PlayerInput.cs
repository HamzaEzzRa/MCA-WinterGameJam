// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""PlayerMain"",
            ""id"": ""ba9e0625-0efd-4630-b9be-8520728f0edf"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""4a51fe3f-59ec-4731-876e-e7c86f80d6ac"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""7ea177f7-005f-43a4-ab7d-6b2175af384b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Absorb"",
                    ""type"": ""Button"",
                    ""id"": ""9f676f95-d5aa-433d-a989-5e92cd8ce5c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""fc0dad08-d949-459a-a20f-23f907d80f31"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""b387c2f4-58f6-4d85-b1c6-818335935d49"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""399d3504-750d-449d-8f7d-d04134a8861d"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""8b082a2a-9b6c-4192-a8e2-1817d5b2bd6f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f0609e29-5d1f-44d6-a444-7989e646a4b9"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""fae546f7-1532-4e10-897f-b376d45f77dc"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ebd62790-cf14-42c5-8516-8a00d8605c70"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4364732b-cae0-4f3c-a616-bf3119e3bfd1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""51653ef8-20f3-4f7f-ac88-4203de3f422f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""92798fe9-d488-4c23-a05e-e04adf795b16"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""daec1d94-5852-48da-9c4e-c15412b3e80e"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2c4b198a-be9a-49f9-9878-e8f61b342a0c"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fa7e0d29-002e-4425-a687-e45528b249a6"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e18874d8-e199-4615-8bcb-16aaf6008a69"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7682ae59-4ddd-4b51-aad4-87bdc1151bcd"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""651d878e-e72b-4578-8523-a36df1948948"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Absorb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""099e77fe-22ab-48e9-a7a4-3929869cd5a1"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa55f8cf-bc3c-44d5-b992-db43c8745451"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""04e0e649-be8a-4ffa-97b5-56be9c12b647"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=20)"",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UIMain"",
            ""id"": ""8207a1ac-c266-4a9d-9afc-282053170289"",
            ""actions"": [
                {
                    ""name"": ""Mouse Position"",
                    ""type"": ""Value"",
                    ""id"": ""edc11a4e-1b70-4785-a6d3-96f64a960659"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""5af611d4-5065-4b80-ac69-746bc589c1d2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cheat Code"",
                    ""type"": ""Button"",
                    ""id"": ""f8ed5273-22b7-47d7-85bc-ffc5c41bd3c3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mouse Focus"",
                    ""type"": ""Button"",
                    ""id"": ""577b0c08-48dc-4a3f-a1de-796d7f5d69aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""780f0f4e-5075-4725-9a03-03d530834b3b"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cf6d9da8-dcc2-480e-845d-9264247f2bd4"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32609431-effc-455a-a4d7-378773216cd5"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cheat Code"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a0571d2-4bf1-43fd-92fa-3742b2ce3465"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse Focus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PC"",
            ""bindingGroup"": ""PC"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Mobile"",
            ""bindingGroup"": ""Mobile"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerMain
        m_PlayerMain = asset.FindActionMap("PlayerMain", throwIfNotFound: true);
        m_PlayerMain_Move = m_PlayerMain.FindAction("Move", throwIfNotFound: true);
        m_PlayerMain_Jump = m_PlayerMain.FindAction("Jump", throwIfNotFound: true);
        m_PlayerMain_Absorb = m_PlayerMain.FindAction("Absorb", throwIfNotFound: true);
        m_PlayerMain_Zoom = m_PlayerMain.FindAction("Zoom", throwIfNotFound: true);
        m_PlayerMain_Look = m_PlayerMain.FindAction("Look", throwIfNotFound: true);
        // UIMain
        m_UIMain = asset.FindActionMap("UIMain", throwIfNotFound: true);
        m_UIMain_MousePosition = m_UIMain.FindAction("Mouse Position", throwIfNotFound: true);
        m_UIMain_Pause = m_UIMain.FindAction("Pause", throwIfNotFound: true);
        m_UIMain_CheatCode = m_UIMain.FindAction("Cheat Code", throwIfNotFound: true);
        m_UIMain_MouseFocus = m_UIMain.FindAction("Mouse Focus", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PlayerMain
    private readonly InputActionMap m_PlayerMain;
    private IPlayerMainActions m_PlayerMainActionsCallbackInterface;
    private readonly InputAction m_PlayerMain_Move;
    private readonly InputAction m_PlayerMain_Jump;
    private readonly InputAction m_PlayerMain_Absorb;
    private readonly InputAction m_PlayerMain_Zoom;
    private readonly InputAction m_PlayerMain_Look;
    public struct PlayerMainActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerMainActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerMain_Move;
        public InputAction @Jump => m_Wrapper.m_PlayerMain_Jump;
        public InputAction @Absorb => m_Wrapper.m_PlayerMain_Absorb;
        public InputAction @Zoom => m_Wrapper.m_PlayerMain_Zoom;
        public InputAction @Look => m_Wrapper.m_PlayerMain_Look;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMain; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMainActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMainActions instance)
        {
            if (m_Wrapper.m_PlayerMainActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnJump;
                @Absorb.started -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnAbsorb;
                @Absorb.performed -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnAbsorb;
                @Absorb.canceled -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnAbsorb;
                @Zoom.started -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnZoom;
                @Look.started -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnLook;
            }
            m_Wrapper.m_PlayerMainActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Absorb.started += instance.OnAbsorb;
                @Absorb.performed += instance.OnAbsorb;
                @Absorb.canceled += instance.OnAbsorb;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
            }
        }
    }
    public PlayerMainActions @PlayerMain => new PlayerMainActions(this);

    // UIMain
    private readonly InputActionMap m_UIMain;
    private IUIMainActions m_UIMainActionsCallbackInterface;
    private readonly InputAction m_UIMain_MousePosition;
    private readonly InputAction m_UIMain_Pause;
    private readonly InputAction m_UIMain_CheatCode;
    private readonly InputAction m_UIMain_MouseFocus;
    public struct UIMainActions
    {
        private @PlayerInput m_Wrapper;
        public UIMainActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MousePosition => m_Wrapper.m_UIMain_MousePosition;
        public InputAction @Pause => m_Wrapper.m_UIMain_Pause;
        public InputAction @CheatCode => m_Wrapper.m_UIMain_CheatCode;
        public InputAction @MouseFocus => m_Wrapper.m_UIMain_MouseFocus;
        public InputActionMap Get() { return m_Wrapper.m_UIMain; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIMainActions set) { return set.Get(); }
        public void SetCallbacks(IUIMainActions instance)
        {
            if (m_Wrapper.m_UIMainActionsCallbackInterface != null)
            {
                @MousePosition.started -= m_Wrapper.m_UIMainActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_UIMainActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_UIMainActionsCallbackInterface.OnMousePosition;
                @Pause.started -= m_Wrapper.m_UIMainActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_UIMainActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_UIMainActionsCallbackInterface.OnPause;
                @CheatCode.started -= m_Wrapper.m_UIMainActionsCallbackInterface.OnCheatCode;
                @CheatCode.performed -= m_Wrapper.m_UIMainActionsCallbackInterface.OnCheatCode;
                @CheatCode.canceled -= m_Wrapper.m_UIMainActionsCallbackInterface.OnCheatCode;
                @MouseFocus.started -= m_Wrapper.m_UIMainActionsCallbackInterface.OnMouseFocus;
                @MouseFocus.performed -= m_Wrapper.m_UIMainActionsCallbackInterface.OnMouseFocus;
                @MouseFocus.canceled -= m_Wrapper.m_UIMainActionsCallbackInterface.OnMouseFocus;
            }
            m_Wrapper.m_UIMainActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @CheatCode.started += instance.OnCheatCode;
                @CheatCode.performed += instance.OnCheatCode;
                @CheatCode.canceled += instance.OnCheatCode;
                @MouseFocus.started += instance.OnMouseFocus;
                @MouseFocus.performed += instance.OnMouseFocus;
                @MouseFocus.canceled += instance.OnMouseFocus;
            }
        }
    }
    public UIMainActions @UIMain => new UIMainActions(this);
    private int m_PCSchemeIndex = -1;
    public InputControlScheme PCScheme
    {
        get
        {
            if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex("PC");
            return asset.controlSchemes[m_PCSchemeIndex];
        }
    }
    private int m_MobileSchemeIndex = -1;
    public InputControlScheme MobileScheme
    {
        get
        {
            if (m_MobileSchemeIndex == -1) m_MobileSchemeIndex = asset.FindControlSchemeIndex("Mobile");
            return asset.controlSchemes[m_MobileSchemeIndex];
        }
    }
    public interface IPlayerMainActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAbsorb(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
    }
    public interface IUIMainActions
    {
        void OnMousePosition(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnCheatCode(InputAction.CallbackContext context);
        void OnMouseFocus(InputAction.CallbackContext context);
    }
}
