using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSyncPosition : NetworkBehaviour {

	[SyncVar] private Vector3 _syncPos;

	[SerializeField] Transform _myTransform;
    [SerializeField] float _damping;

	void Update () 
    {
		TransmitPosition ();
		LerpPosition ();
	}

	void LerpPosition()
	{
		if (!isLocalPlayer)
		{
			_myTransform.position = Vector3.Lerp (_myTransform.position, _syncPos, _damping);
		}

	}

	[Command]
	void CmdProvidePositionToServer(Vector3 pos)
	{
		_syncPos = pos;
	}

	[ClientCallback]
	void TransmitPosition()
	{
		if (isLocalPlayer) 
		{
			CmdProvidePositionToServer (transform.position);
		}
	}
}
