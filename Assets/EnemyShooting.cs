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
	}

	void Shoot()
	{

	}
}
