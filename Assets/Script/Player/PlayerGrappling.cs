using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrappling : MonoBehaviour
{
    [Header("GrapplingGun")]
    [SerializeField] private GrappingRope grappleRopeObject;
    [SerializeField] private SpringJoint2D m_springJoint2D;

    public GrappingRope GrappingRopeObject
    {
        get { return grappleRopeObject; }
        set { grappleRopeObject = value; }
    }

    public SpringJoint2D MSpringJoint2D
    {
        get { return m_springJoint2D; }
        set { m_springJoint2D = value; }
    }
}
