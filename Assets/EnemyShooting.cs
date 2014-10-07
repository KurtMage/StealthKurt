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
}
