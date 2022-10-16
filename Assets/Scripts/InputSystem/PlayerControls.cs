// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputSystem/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""BaseControls"",
            ""id"": ""5cedd6d8-8a76-451e-bd32-05f31b4a291e"",
            ""actions"": [
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""04e305e7-4549-47bb-9cd8-2485db491aa3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""344e10b9-99d9-4106-906e-1bf0f057005a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MiddleClick"",
                    ""type"": ""Button"",
                    ""id"": ""194319e1-aee2-4961-a83b-4acaa9fc50ea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ScreenMove"",
                    ""type"": ""Value"",
                    ""id"": ""bb89f96c-0a95-45d0-978d-c1d1450a9b9d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwapSelected"",
                    ""type"": ""Button"",
                    ""id"": ""1adbcd8c-9cf3-4939-863a-23dedf5e04f5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3c56dc46-4685-433f-9a82-9212c1252c4e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""96a53a2b-815e-4053-8452-7fa1ef857902"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7652d4c0-88f0-4acb-869d-24ea4e750632"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MiddleClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""fd0b45a9-1eb0-422f-abbd-654321582d15"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScreenMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""34168358-a409-4dde-9284-66b4136017cd"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScreenMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b44cd327-ecc1-4b33-8b9c-fc797ad7ec40"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScreenMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0131604b-7de1-4088-bccb-aef501fb2a98"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScreenMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""aeb4095f-14b6-48a0-8cc8-3d02643e2819"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScreenMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4a1d370c-e4de-4d58-9ce0-22684654b6f4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwapSelected"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // BaseControls
        m_BaseControls = asset.FindActionMap("BaseControls", throwIfNotFound: true);
        m_BaseControls_LeftClick = m_BaseControls.FindAction("LeftClick", throwIfNotFound: true);
        m_BaseControls_RightClick = m_BaseControls.FindAction("RightClick", throwIfNotFound: true);
        m_BaseControls_MiddleClick = m_BaseControls.FindAction("MiddleClick", throwIfNotFound: true);
        m_BaseControls_ScreenMove = m_BaseControls.FindAction("ScreenMove", throwIfNotFound: true);
        m_BaseControls_SwapSelected = m_BaseControls.FindAction("SwapSelected", throwIfNotFound: true);
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

    // BaseControls
    private readonly InputActionMap m_BaseControls;
    private IBaseControlsActions m_BaseControlsActionsCallbackInterface;
    private readonly InputAction m_BaseControls_LeftClick;
    private readonly InputAction m_BaseControls_RightClick;
    private readonly InputAction m_BaseControls_MiddleClick;
    private readonly InputAction m_BaseControls_ScreenMove;
    private readonly InputAction m_BaseControls_SwapSelected;
    public struct BaseControlsActions
    {
        private @PlayerControls m_Wrapper;
        public BaseControlsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftClick => m_Wrapper.m_BaseControls_LeftClick;
        public InputAction @RightClick => m_Wrapper.m_BaseControls_RightClick;
        public InputAction @MiddleClick => m_Wrapper.m_BaseControls_MiddleClick;
        public InputAction @ScreenMove => m_Wrapper.m_BaseControls_ScreenMove;
        public InputAction @SwapSelected => m_Wrapper.m_BaseControls_SwapSelected;
        public InputActionMap Get() { return m_Wrapper.m_BaseControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BaseControlsActions set) { return set.Get(); }
        public void SetCallbacks(IBaseControlsActions instance)
        {
            if (m_Wrapper.m_BaseControlsActionsCallbackInterface != null)
            {
                @LeftClick.started -= m_Wrapper.m_BaseControlsActionsCallbackInterface.OnLeftClick;
                @LeftClick.performed -= m_Wrapper.m_BaseControlsActionsCallbackInterface.OnLeftClick;
                @LeftClick.canceled -= m_Wrapper.m_BaseControlsActionsCallbackInterface.OnLeftClick;
                @RightClick.started -= m_Wrapper.m_BaseControlsActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_BaseControlsActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_BaseControlsActionsCallbackInterface.OnRightClick;
                @MiddleClick.started -= m_Wrapper.m_BaseControlsActionsCallbackInterface.OnMiddleClick;
                @MiddleClick.performed -= m_Wrapper.m_BaseControlsActionsCallbackInterface.OnMiddleClick;
                @MiddleClick.canceled -= m_Wrapper.m_BaseControlsActionsCallbackInterface.OnMiddleClick;
                @ScreenMove.started -= m_Wrapper.m_BaseControlsActionsCallbackInterface.OnScreenMove;
                @ScreenMove.performed -= m_Wrapper.m_BaseControlsActionsCallbackInterface.OnScreenMove;
                @ScreenMove.canceled -= m_Wrapper.m_BaseControlsActionsCallbackInterface.OnScreenMove;
                @SwapSelected.started -= m_Wrapper.m_BaseControlsActionsCallbackInterface.OnSwapSelected;
                @SwapSelected.performed -= m_Wrapper.m_BaseControlsActionsCallbackInterface.OnSwapSelected;
                @SwapSelected.canceled -= m_Wrapper.m_BaseControlsActionsCallbackInterface.OnSwapSelected;
            }
            m_Wrapper.m_BaseControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftClick.started += instance.OnLeftClick;
                @LeftClick.performed += instance.OnLeftClick;
                @LeftClick.canceled += instance.OnLeftClick;
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
                @MiddleClick.started += instance.OnMiddleClick;
                @MiddleClick.performed += instance.OnMiddleClick;
                @MiddleClick.canceled += instance.OnMiddleClick;
                @ScreenMove.started += instance.OnScreenMove;
                @ScreenMove.performed += instance.OnScreenMove;
                @ScreenMove.canceled += instance.OnScreenMove;
                @SwapSelected.started += instance.OnSwapSelected;
                @SwapSelected.performed += instance.OnSwapSelected;
                @SwapSelected.canceled += instance.OnSwapSelected;
            }
        }
    }
    public BaseControlsActions @BaseControls => new BaseControlsActions(this);
    public interface IBaseControlsActions
    {
        void OnLeftClick(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
        void OnMiddleClick(InputAction.CallbackContext context);
        void OnScreenMove(InputAction.CallbackContext context);
        void OnSwapSelected(InputAction.CallbackContext context);
    }
}
