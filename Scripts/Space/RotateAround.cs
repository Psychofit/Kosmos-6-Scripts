using UnityEngine;
using System.Collections;

namespace Cosmos_Six
{
	public class RotateAround : MonoBehaviour
	{

		public Transform target;
		public int speedRotation;
		public float speedMove = 0.5f;
		private IEnumerator coroutineMoveSun;
		private WaitForSeconds waitForMoveSun;
		[SerializeField] private float waitForMoveSunDelay = 0.1f;


		private void Start()
		{
			if (target == null)
			{
				target = this.gameObject.transform;
			}
			waitForMoveSun = new WaitForSeconds(waitForMoveSunDelay);
			StartCoroutineMoveSun();
		}

		private void Update()
		{
			RotateArroundSmth();
		}

		private void MoveSun()
		{
			if (gameObject.name == "Sun")
			{
				transform.Translate(Vector3.forward * speedMove * Time.deltaTime);
			}
		}

		private void RotateArroundSmth()
		{
			transform.RotateAround(target.transform.position, target.transform.up, speedRotation * Time.deltaTime);
		}

		[ContextMenu("StartCoroutineMoveSun")]
		public void StartCoroutineMoveSun()
		{
			coroutineMoveSun = MoveSunCor();
			StartCoroutine(coroutineMoveSun);
		}

		[ContextMenu("StopCoroutineMoveSun")]
		public void StopCoroutineMoveSun()
		{
			StopCoroutine(coroutineMoveSun);
		}

		private IEnumerator MoveSunCor()
		{
			while (true)
			{
				yield return waitForMoveSun;
				MoveSun();
			}
		}
	}
}