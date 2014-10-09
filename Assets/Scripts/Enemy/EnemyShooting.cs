using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour {

	public float maxDamage = 120f;
	public float minDamage = 45f;
	public AudioClip shotClip;
	public float flashIntensity = 3f;
	public float fadeSpeed = 10f;

	private Animator anim;
	private HashIDs hash;
	private LineRenderer laserShotLine;
	private Light laserShotLight;
	private SphereCollider col;
	private Transform player;
	private PlayerHealth playerHealth;
	private bool shooting;
	private float scaledDamage;

	void Awake()
	{
		anim = GetComponent<Animator> ();
		hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<HashIDs> ();
		laserShotLine = GetComponentInChildren<LineRenderer> ();
		laserShotLight = laserShotLine.gameObject.light;
		col = GetComponent<SphereCollider> ();
		GameObject playerGameObject = GameObject.FindGameObjectWithTag (Tags.player);
		player = playerGameObject.transform;
		playerHealth = playerGameObject.GetComponent<PlayerHealth> ();
		shooting = false;

		laserShotLine.enabled = false;
		laserShotLight.intensity = 0f;

		scaledDamage = maxDamage - minDamage;

	}

	void Update()
	{
		float shot = anim.GetFloat (hash.shotFloat);

		if (shot > 0.5f && !shooting)
		{
			Shoot();
		}

		if (shot < 0.5f)
		{
			shooting = false;
			laserShotLine.enabled = false;
		}

		laserShotLight.intensity = Mathf.Lerp (laserShotLight.intensity, 0, Time.deltaTime * fadeSpeed);
	}

	void OnAnimatorIK(int layerIndex)
	{
		float aimWeight = anim.GetFloat (hash.aimWeightFloat);
		anim.SetIKPosition (AvatarIKGoal.RightHand, player.position + Vector3.up * 1.5f);
		anim.SetIKPositionWeight (AvatarIKGoal.RightHand, aimWeight);
	}

	void Shoot()
	{
		shooting = true;
		float fractionalDistance = (col.radius - Vector3.Distance(transform.position, player.position)) / col.radius;
		float damage = scaledDamage * fractionalDistance + minDamage;
		playerHealth.TakeDamage (damage);
		ShotEffects ();

	}

	void ShotEffects()
	{
		laserShotLine.SetPosition (0, laserShotLine.transform.position);
		laserShotLine.SetPosition (1, player.position + Vector3.up * 1.5f);
		laserShotLine.enabled = true;
		laserShotLight.intensity = flashIntensity;
		AudioSource.PlayClipAtPoint (shotClip, laserShotLine.transform.position);
	}
}
