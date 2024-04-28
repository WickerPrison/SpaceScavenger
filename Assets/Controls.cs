//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Controls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Combat"",
            ""id"": ""41a8e246-471a-4896-9e48-53bfc5e4f231"",
            ""actions"": [
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""19ef7118-6798-4333-8fb5-60749639848a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveCamera"",
                    ""type"": ""Value"",
                    ""id"": ""d01ae941-684b-40e1-883f-16678a1e6241"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""EndTurn"",
                    ""type"": ""Button"",
                    ""id"": ""07b652c1-06fe-4645-9f42-a075c68ad2a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Ability1"",
                    ""type"": ""Button"",
                    ""id"": ""cd39ad19-8de9-4ff0-854b-a26142a56596"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Ability2"",
                    ""type"": ""Button"",
                    ""id"": ""1937c493-2529-4980-ac38-28cd907683de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Ability3"",
                    ""type"": ""Button"",
                    ""id"": ""8e3029c1-6910-4039-914b-5e238255de2a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Ability4"",
                    ""type"": ""Button"",
                    ""id"": ""6f5df9ef-54c4-44c5-8096-5a1a84662a56"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""faf17f06-3bb7-43df-a6d0-d0cddc3ad829"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""3e33024f-c6db-40f1-8461-37f4e534440c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""023093db-e63b-48b0-a251-573476579f74"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""9b1de221-aa2a-4794-a615-fbda297cd6c6"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""72e126f1-ed4a-41d5-8ea3-70ef4468d63c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0ee0f210-f9f3-4df3-9404-59ad2eeb422d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c917a79d-e693-4e46-9421-f1cf93df1915"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""92d88e3f-57b3-4bb9-ac92-8c66025191d4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c313775b-bdcc-4729-92b7-a9f7e404e917"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EndTurn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de68f007-e208-4c08-9f31-e0b1bd7496a9"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f2642e7-438b-4833-813e-a3b53c68b93d"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""822cfd8f-2096-4ae3-b0d2-959ca6833d89"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94e1055d-925a-4c7d-81b6-3078a0bcfca5"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9ea1f21-464c-48fb-80b9-e26dfbedd69a"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""353c6a2e-7ede-4b66-9b46-34afd472a777"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Combat
        m_Combat = asset.FindActionMap("Combat", throwIfNotFound: true);
        m_Combat_LeftClick = m_Combat.FindAction("LeftClick", throwIfNotFound: true);
        m_Combat_MoveCamera = m_Combat.FindAction("MoveCamera", throwIfNotFound: true);
        m_Combat_EndTurn = m_Combat.FindAction("EndTurn", throwIfNotFound: true);
        m_Combat_Ability1 = m_Combat.FindAction("Ability1", throwIfNotFound: true);
        m_Combat_Ability2 = m_Combat.FindAction("Ability2", throwIfNotFound: true);
        m_Combat_Ability3 = m_Combat.FindAction("Ability3", throwIfNotFound: true);
        m_Combat_Ability4 = m_Combat.FindAction("Ability4", throwIfNotFound: true);
        m_Combat_Back = m_Combat.FindAction("Back", throwIfNotFound: true);
        m_Combat_Interact = m_Combat.FindAction("Interact", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Combat
    private readonly InputActionMap m_Combat;
    private ICombatActions m_CombatActionsCallbackInterface;
    private readonly InputAction m_Combat_LeftClick;
    private readonly InputAction m_Combat_MoveCamera;
    private readonly InputAction m_Combat_EndTurn;
    private readonly InputAction m_Combat_Ability1;
    private readonly InputAction m_Combat_Ability2;
    private readonly InputAction m_Combat_Ability3;
    private readonly InputAction m_Combat_Ability4;
    private readonly InputAction m_Combat_Back;
    private readonly InputAction m_Combat_Interact;
    public struct CombatActions
    {
        private @Controls m_Wrapper;
        public CombatActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftClick => m_Wrapper.m_Combat_LeftClick;
        public InputAction @MoveCamera => m_Wrapper.m_Combat_MoveCamera;
        public InputAction @EndTurn => m_Wrapper.m_Combat_EndTurn;
        public InputAction @Ability1 => m_Wrapper.m_Combat_Ability1;
        public InputAction @Ability2 => m_Wrapper.m_Combat_Ability2;
        public InputAction @Ability3 => m_Wrapper.m_Combat_Ability3;
        public InputAction @Ability4 => m_Wrapper.m_Combat_Ability4;
        public InputAction @Back => m_Wrapper.m_Combat_Back;
        public InputAction @Interact => m_Wrapper.m_Combat_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Combat; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CombatActions set) { return set.Get(); }
        public void SetCallbacks(ICombatActions instance)
        {
            if (m_Wrapper.m_CombatActionsCallbackInterface != null)
            {
                @LeftClick.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnLeftClick;
                @LeftClick.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnLeftClick;
                @LeftClick.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnLeftClick;
                @MoveCamera.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnMoveCamera;
                @EndTurn.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnEndTurn;
                @EndTurn.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnEndTurn;
                @EndTurn.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnEndTurn;
                @Ability1.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnAbility1;
                @Ability1.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnAbility1;
                @Ability1.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnAbility1;
                @Ability2.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnAbility2;
                @Ability2.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnAbility2;
                @Ability2.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnAbility2;
                @Ability3.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnAbility3;
                @Ability3.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnAbility3;
                @Ability3.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnAbility3;
                @Ability4.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnAbility4;
                @Ability4.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnAbility4;
                @Ability4.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnAbility4;
                @Back.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnBack;
                @Interact.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_CombatActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftClick.started += instance.OnLeftClick;
                @LeftClick.performed += instance.OnLeftClick;
                @LeftClick.canceled += instance.OnLeftClick;
                @MoveCamera.started += instance.OnMoveCamera;
                @MoveCamera.performed += instance.OnMoveCamera;
                @MoveCamera.canceled += instance.OnMoveCamera;
                @EndTurn.started += instance.OnEndTurn;
                @EndTurn.performed += instance.OnEndTurn;
                @EndTurn.canceled += instance.OnEndTurn;
                @Ability1.started += instance.OnAbility1;
                @Ability1.performed += instance.OnAbility1;
                @Ability1.canceled += instance.OnAbility1;
                @Ability2.started += instance.OnAbility2;
                @Ability2.performed += instance.OnAbility2;
                @Ability2.canceled += instance.OnAbility2;
                @Ability3.started += instance.OnAbility3;
                @Ability3.performed += instance.OnAbility3;
                @Ability3.canceled += instance.OnAbility3;
                @Ability4.started += instance.OnAbility4;
                @Ability4.performed += instance.OnAbility4;
                @Ability4.canceled += instance.OnAbility4;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public CombatActions @Combat => new CombatActions(this);
    public interface ICombatActions
    {
        void OnLeftClick(InputAction.CallbackContext context);
        void OnMoveCamera(InputAction.CallbackContext context);
        void OnEndTurn(InputAction.CallbackContext context);
        void OnAbility1(InputAction.CallbackContext context);
        void OnAbility2(InputAction.CallbackContext context);
        void OnAbility3(InputAction.CallbackContext context);
        void OnAbility4(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}
