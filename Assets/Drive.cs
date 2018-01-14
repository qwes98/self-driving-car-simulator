using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Car;

	[RequireComponent(typeof(CarController))]
	public class Drive : MonoBehaviour {
		private CarController m_Car;
		

		public float speed = 1.0F;
		public float rotationSpeed = 1.0F;

		private void Awake() {
			m_Car = GetComponent<CarController>();
            
		}

		void Update () {
			float translation = CrossPlatformInputManager.GetAxis("Vertical") * speed;
			float rotation = CrossPlatformInputManager.GetAxis("Horizontal") * rotationSpeed;
			translation *= Time.deltaTime;
			rotation *= Time.deltaTime;
			//transform.Translate(0, 0, translation);
			//transform.Rotate(0, rotation, 0);

            m_Car.Move(rotation, translation, 0f, 0f);
		}
	}

/* 
	public class Drive : MonoBehaviour {

		public float speed = 1.0F;
		public float rotationSpeed = 1.0F;
		void Update () {
			float translation = CrossPlatformInputManager.GetAxis("Vertical") * speed;
			float rotation = CrossPlatformInputManager.GetAxis("Horizontal") * rotationSpeed;
			translation *= Time.deltaTime;
			rotation *= Time.deltaTime;
			transform.Translate(0, 0, translation);
			transform.Rotate(0, rotation, 0);
			
		}
	}
	*/