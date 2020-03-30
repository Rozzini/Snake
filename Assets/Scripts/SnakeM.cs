using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SnakeM : MonoBehaviour
{	
	bool ate = false;

	bool isDied = false;

	bool vertical = false;

	bool horizontal = true;

	public GameObject TailPrefab;

	Vector2 dir = Vector2.right;

	List<Transform> tail = new List<Transform>();

	void Start()
	{
		InvokeRepeating("Move", 0.3f, 0.3f);
	}

	void Update()
	{
		if (!isDied)
		{
			if (Input.GetKey(KeyCode.RightArrow) && horizontal)
			{
				horizontal = false;
				vertical = true;
				dir = Vector2.right;
			}
			else if (Input.GetKey(KeyCode.UpArrow) && vertical)
			{
				horizontal = true;
				vertical = false;
				dir = Vector2.up;
			}
			else if (Input.GetKey(KeyCode.DownArrow) && vertical)
			{
				horizontal = true;
				vertical = false;
				dir = -Vector2.up;
			}
			else if (Input.GetKey(KeyCode.LeftArrow) && horizontal)
			{
				horizontal = false;
				vertical = true;
				dir = -Vector2.right;
			}			
		}
		else
		{
			if (Input.GetKey(KeyCode.R))
			{
				
				tail.Clear();

				var gameObjects = GameObject.FindGameObjectsWithTag("Finish");
				foreach(var target in gameObjects)
				{
					GameObject.Destroy(target);
				}
				
				transform.position = new Vector3(0, 0, 0);

				
				isDied = false;
			}
		}
	}

	void Move()
	{
		if (!isDied)
		{
			Vector2 v = transform.position;

			transform.Translate(dir);

			if (ate)
			{
				GameObject g = (GameObject)Instantiate(TailPrefab,
								  v,
								  Quaternion.identity);

				tail.Insert(0, g.transform);

				ate = false;
			}
			else if (tail.Count > 0)
			{   
				tail.Last().position = v;

				tail.Insert(0, tail.Last());
				tail.RemoveAt(tail.Count - 1);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.name.StartsWith("Food"))
		{
			ate = true;

			Destroy(coll.gameObject);
		}
		else
		{   
			isDied = true;
		}
	}
}
