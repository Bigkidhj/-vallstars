using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeamManager : MonoBehaviour
{
    // 팀장 클래스 정의
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

    // 팀원 클래스 정의
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

    // 팀장과 팀원 리스트
    public List<TeamLeader> teamLeaders;
    public List<TeamMember> teamMembers;
    // 팀장 이름 리스트
    public List<TMP_Text> leaderNames;

    public void Start()
    {
        // 팀장 리스트 초기화
        teamLeaders = new List<TeamLeader>
        {
            new TeamLeader("팀장A", 0),
            new TeamLeader("팀장B", 1),
            new TeamLeader("팀장C", 2),
            new TeamLeader("팀장D", 3),
            new TeamLeader("팀장E", 4),
            new TeamLeader("팀장F", 5),
            new TeamLeader("팀장G", 6),
            new TeamLeader("팀장H", 7),
            new TeamLeader("팀장I", 8),
            new TeamLeader("팀장J", 9)
        };

        // 팀원 리스트 초기화
        teamMembers = new List<TeamMember>
        {
            new TeamMember("팀원A", 1),
            new TeamMember("팀원B", 2),
            new TeamMember("팀원C", 3),
            new TeamMember("팀원D", 4),
            new TeamMember("팀원E", 5)
        };

        // 팀장 이름 리스트의 텍스트를 팀장 리스트에 있는 이름으로 설정
        for (int i = 0; i < leaderNames.Count && i < teamLeaders.Count; i++)
        {
            leaderNames[i].text = teamLeaders[i].name;
        }
    }
}
