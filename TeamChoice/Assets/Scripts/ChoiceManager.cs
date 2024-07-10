using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    public TeamManager teamManager;
    public TeamManager.TeamMember teamMember;
    public void Start()
    {
        //TeamManager를 찾아서 teamManager에 할당
        teamManager = GameObject.Find("TeamManager").GetComponent<TeamManager>();
    }
}
