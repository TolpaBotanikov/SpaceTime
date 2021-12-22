using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int roomCount;
    public bool finished = false;
    [Header("Не трогайте это!!!")]
    public List<ModuleJoint> joints = new List<ModuleJoint>();
    public List<Collider> colliders = new List<Collider>();
    public GameObject[] modules;
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

    IEnumerator GenerateLevel()
    {
        yield return new WaitForSeconds(60);
        finished = true;
    }

    IEnumerator ConstructMosules()
    {
        while (roomCount > 0)
        {
            JoinModule();
            yield return new WaitForEndOfFrame();
        }
        if (roomCount == 0) finished = true;
    }

    void JoinModule()
    {
        if (finished) return;
        ModuleJoint joint = joints[Random.Range(0, joints.Count)];
        Direction jointDirection = joint.direction;
        List<GameObject> avaliableModules = modules.Where(
            m => m.GetComponent<Joinable>().directions.
            Contains(jointDirection)).ToList();
        GameObject moduleGo = Instantiate(avaliableModules[Random.Range(0, avaliableModules.Count)], new Vector3(100, 100, 100), Quaternion.Euler(0,0,0));
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

        //Collider[] colls = Physics.OverlapBox(collider.center, collider.bounds.extents / 2).Where(c => c.gameObject.GetComponent<Joinable>() != null).ToArray();
        //if (colls.Length > 1)
        foreach (Collider coll in colliders)
        if (collider.bounds.Intersects(coll.bounds))
        {
            print("Intersection!!! In " + moduleGo + " " + roomCount + " with " + coll/*s[0]*/);
        }


        joint.door.closed = false;
        colliders.Add(collider);
        joints.Remove(joint);
        joints.AddRange(module.joints.Where(j => j.direction != OppositeDirection(jointDirection)));
        roomCount--;
    }

    private void OnDrawGizmos()
    {
        Collider collider = GetComponent<Collider>();
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(collider.bounds.center, collider.bounds.size);
    }

    Direction OppositeDirection(Direction direction)
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
