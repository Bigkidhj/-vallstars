using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    // ÆÀÀå Å¬·¡½º Á¤ÀÇ
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

    // ÆÀ¿ø Å¬·¡½º Á¤ÀÇ
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

    // ÆÀÀå°ú ÆÀ¿ø ¸®½ºÆ®
    public List<TeamLeader> teamLeaders;
    public List<TeamMember> teamMembers;

    public void Start()
    {
        // ÆÀÀå ¸®½ºÆ® ÃÊ±âÈ­
        teamLeaders = new List<TeamLeader>
        {
            new TeamLeader("ÆÀÀåA", 1),
            new TeamLeader("ÆÀÀåB", 2),
            new TeamLeader("ÆÀÀåC", 3),
        };

        // ÆÀ¿ø ¸®½ºÆ® ÃÊ±âÈ­
        teamMembers = new List<TeamMember>
        {
            new TeamMember("ÆÀ¿øA", 1),
            new TeamMember("ÆÀ¿øB", 2),
            new TeamMember("ÆÀ¿øC", 3),
            new TeamMember("ÆÀ¿øD", 4),
            new TeamMember("ÆÀ¿øE", 5)
        };

        // ÆÀÀå°ú ÆÀ¿ø ¸®½ºÆ® Ãâ·Â
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
