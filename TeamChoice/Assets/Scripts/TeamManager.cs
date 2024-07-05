using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    // ���� Ŭ���� ����
    [System.Serializable]
    public class TeamLeader
    {
        public string name;
        public int order;

        public TeamLeader(string name, int order)
        {
            this.name = name;
            this.order = order;
        }
    }

    // ���� Ŭ���� ����
    [System.Serializable]
    public class TeamMember
    {
        public string name;
        public int order;

        public TeamMember(string name, int order)
        {
            this.name = name;
            this.order = order;
        }
    }

    // ����� ���� ����Ʈ
    public List<TeamLeader> teamLeaders;
    public List<TeamMember> teamMembers;

    public void Start()
    {
        // ���� ����Ʈ �ʱ�ȭ
        teamLeaders = new List<TeamLeader>
        {
            new TeamLeader("����A", 1),
            new TeamLeader("����B", 2),
            new TeamLeader("����C", 3),
        };

        // ���� ����Ʈ �ʱ�ȭ
        teamMembers = new List<TeamMember>
        {
            new TeamMember("����A", 1),
            new TeamMember("����B", 2),
            new TeamMember("����C", 3),
            new TeamMember("����D", 4),
            new TeamMember("����E", 5)
        };

        // ����� ���� ����Ʈ ���
        Debug.Log("Team Leaders: ");
        foreach(var leader in teamLeaders)
        {
            Debug.Log($"Name: {leader.name}, Order: {leader.order}");
        }

        Debug.Log("Team Members:");
        foreach (var member in teamMembers)
        {
            Debug.Log($"Name: {member.name}, Order: {member.order}");
        }
    }
}
