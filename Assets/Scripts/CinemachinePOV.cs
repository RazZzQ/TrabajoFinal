using UnityEngine;
using Cinemachine;

public class CinemachinePOV : CinemachineExtension
{
    public PlayerMovement Player;
    private Vector3 startingRotation;
    protected override void Awake()
    {
        
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if(stage == CinemachineCore.Stage.Aim)
            {
                if (startingRotation == null) startingRotation = transform.localRotation.eulerAngles;
            }
        }
    }
}
