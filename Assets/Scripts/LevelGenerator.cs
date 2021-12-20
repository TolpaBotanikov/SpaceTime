using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int roomCount;
    [Header("Не трогайте это!!!")]
    public List<ModuleJoint> joints = new List<ModuleJoint>();
    public GameObject[] modules;
    public GameObject main;
    private void Update()
    {
        //foreach(GameObject joint in joints)
        //{
        //    GameObject corridorGo = Instantiate(modules[Random.Range(0, modules.Length)]);

        //    Corridor corridor = corridorGo.GetComponent<Corridor>();

        //    GameObject corridorJoint = corridor.joints[Random.Range(0, corridor.joints.Length)].gameObject;
        //    corridorJoint = corridor.joints[0].gameObject;
            
        //    corridorGo.transform.SetParent(main.transform);

        //    Vector3 position = joint.transform.position;
        //    position.x = joint.transform.position.x - corridorJoint.transform.localPosition.x;
        //    position.z = joint.transform.position.z - corridorJoint.transform.localPosition.z;
        //    corridorGo.transform.position = position;

        //    //corridor.transform.position =
        //}
        if (roomCount > 0)
        {
            JoinModule();
            roomCount--;
        }
    }

    void JoinModule()
    {
        ModuleJoint joint = joints[Random.Range(0, joints.Count)];
        Direction jointDirection = joint.direction;
        List<GameObject> avaliableModules = modules.Where(
            m => m.GetComponent<Joinable>().directions.
            Contains(jointDirection)).ToList();
        GameObject moduleGo = Instantiate(avaliableModules[Random.Range(0, avaliableModules.Count)]);
        Joinable module = moduleGo.GetComponent<Joinable>();

        ModuleJoint moduleJoint = module.joints.FirstOrDefault(j => j.direction == OppositeDirection(jointDirection));

        moduleGo.transform.SetParent(main.transform);

        

        Vector3 position = joint.transform.position;
        position.y = main.transform.position.y;
        position.x = joint.transform.position.x - moduleJoint.transform.localPosition.x;
        position.z = joint.transform.position.z - moduleJoint.transform.localPosition.z;
        moduleGo.transform.position = position;

        joints.Remove(joint);
        joints.AddRange(module.joints.Where(j => j.direction != OppositeDirection(jointDirection)));
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
