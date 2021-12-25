using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public int roomCount;
    [Header("Не трогайте это!!!")]
    public bool finished = false;
    public List<string> requiredModules = new List<string>();
    public List<ModuleJoint> joints = new List<ModuleJoint>();
    public List<Collider> colliders = new List<Collider>();
    public List<GameObject> generatedModules = new List<GameObject>();
    public List<GameObject> modules;
    public GameObject main;
    public GameObject[] IntersectionIgnorable;
    public static LevelGenerator S;

    private void Awake()
    {
        S = this;
        colliders.Add(GetComponent<Collider>());
        StartCoroutine(GenerateLevel());
        StartCoroutine(ConstructMosules());
    }
    private void Update()
    {
        //StartCoroutine(GenerateLevel());
        //if (roomCount > 0)
        //{
        //    JoinModule();
        //}
        //if (roomCount == 0) finished = true;
    }

    private IEnumerator GenerateLevel()
    {
        yield return new WaitForSeconds(30);
        finished = true;
    }

    private IEnumerator ConstructMosules()
    {
        while (roomCount > 0 && !finished)
        {
            JoinModule();
            yield return new WaitForEndOfFrame();
        }
        if (roomCount == 0)
        {
            finished = true;
            bool regeneration = false;
            foreach(string module in requiredModules)
            {
                if (generatedModules.Where(m => m.name.Contains(module)).Count() == 0)
                {
                    regeneration = true;
                    SceneManager.LoadScene("GenerationTest");
                }
            }
            // Удалить
            if (!regeneration)
                Game.S.StartGame();
        }
    }

    private void JoinModule()
    {
        if (finished) return;
        ModuleJoint joint = joints[Random.Range(0, joints.Count)];
        Direction jointDirection = joint.direction;
        List<GameObject> avaliableModules = modules.Where(
            m => m.GetComponent<Joinable>().directions.
            Contains(jointDirection)).ToList();
        GameObject modulePrefab = avaliableModules[Random.Range(0, avaliableModules.Count)];
        GameObject moduleGo = Instantiate(modulePrefab, new Vector3(100, 100, 100), Quaternion.Euler(0,0,0));
        moduleGo.name += roomCount;
        Joinable module = moduleGo.GetComponent<Joinable>();
        module.joint = joint;
        module.number = roomCount;

        ModuleJoint moduleJoint = module.joints.FirstOrDefault(j => j.direction == OppositeDirection(jointDirection));

        BoxCollider collider = moduleGo.GetComponent<BoxCollider>();

        if (joint == null)
        {
            Destroy(moduleGo);
            return;
        }

        Vector3 position = joint.transform.position;
        position.y = main.transform.position.y;
        position.x = joint.transform.position.x - moduleJoint.transform.localPosition.x;
        position.z = joint.transform.position.z - moduleJoint.transform.localPosition.z;
        moduleGo.transform.position = position;

        joint.door.closed = false;
        moduleJoint.door.closed = false;
        colliders.Add(collider);
        joints.Remove(joint);
        joints.AddRange(module.joints.Where(j => j.direction != OppositeDirection(jointDirection)));
        generatedModules.Add(moduleGo);
        roomCount--;

        if (module.unique)
        {
            foreach (GameObject go in module.variants.examples)
            {
                modules.Remove(go);
                print(go + " removed");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Collider collider = GetComponent<Collider>();
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(collider.bounds.center, collider.bounds.size);
    }

    private Direction OppositeDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.bottom:
                return Direction.top;
            case Direction.top:
                return Direction.bottom;
            case Direction.left:
                return Direction.right;
            case Direction.right:
                return Direction.left;
        }
        return Direction.top;
    }
}
