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

    //팀 클래스 정의
    [System.Serializable]
      public class Team
        {
            public List<TeamLeader> leader;
            public List<TeamMember> members;

            public Team(List<TeamLeader> leader, List<TeamMember> members)
            {
                this.leader = leader;
                this.members = members;
            }
        }

    // 팀장과 팀원 리스트
    public List<TeamLeader> teamLeaders;
    public List<TeamMember> teamMembers;
    // 팀장 이미지 리스트
    public List<Image> leaderImages;
    // 팀장 이름 리스트
    public List<TMP_Text> leaderNames;
    // 팀 리스트
    public Team teamA;
    public Team teamB;
    public Team teamC;
    public Team teamD;
    public Team teamE;    
    public Team teamF;
    public Team teamG;
    public Team teamH;
    public Team teamI;
    public Team teamJ;

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
            new TeamMember("팀원1", 0),
            new TeamMember("팀원2", 1),
            new TeamMember("팀원3", 2),
            new TeamMember("팀원4", 3),
            new TeamMember("팀원5", 4),
            new TeamMember("팀원6", 5),
            new TeamMember("팀원7", 6),
            new TeamMember("팀원8", 7),
            new TeamMember("팀원9", 8),
            new TeamMember("팀원10", 9),
            new TeamMember("팀원11", 10),
            new TeamMember("팀원12", 11),
            new TeamMember("팀원13", 12),
            new TeamMember("팀원14", 13),
            new TeamMember("팀원15", 14),
            new TeamMember("팀원16", 15),
            new TeamMember("팀원17", 16),
            new TeamMember("팀원18", 17),
            new TeamMember("팀원19", 18),
            new TeamMember("팀원20", 19)
        };

        //각 팀 리스트에 팀장과 팀원 리스트를 할당, 단 팀장은 한명씩 할당되며 팀원 리스트는 비어있음
        teamA = new Team(teamLeaders.GetRange(0, 1), new List<TeamMember>());
        teamB = new Team(teamLeaders.GetRange(1, 1), new List<TeamMember>());
        teamC = new Team(teamLeaders.GetRange(2, 1), new List<TeamMember>());
        teamD = new Team(teamLeaders.GetRange(3, 1), new List<TeamMember>());
        teamE = new Team(teamLeaders.GetRange(4, 1), new List<TeamMember>());
        teamF = new Team(teamLeaders.GetRange(5, 1), new List<TeamMember>());
        teamG = new Team(teamLeaders.GetRange(6, 1), new List<TeamMember>());
        teamH = new Team(teamLeaders.GetRange(7, 1), new List<TeamMember>());
        teamI = new Team(teamLeaders.GetRange(8, 1), new List<TeamMember>());
        teamJ = new Team(teamLeaders.GetRange(9, 1), new List<TeamMember>());
        

        // 팀장 이름 리스트의 텍스트를 팀장 리스트에 있는 이름으로 설정
        for (int i = 0; i < leaderNames.Count && i < teamLeaders.Count; i++)
        {
            leaderNames[i].text = teamLeaders[i].name;
        }
        //팀장 이미지 리스트의 이미지를 게임오브젝트와 이름이 같은 이미지를 에셋의 이미지 폴더 안에 있는 TeamLeader 폴더에서 찾아서 할당
        for (int i = 0; i < leaderImages.Count && i < teamLeaders.Count; i++)
        {
            leaderImages[i].sprite = Resources.Load<Sprite>("Images/TeamLeader/" + teamLeaders[i].name);
        }
        // 첫번째 멤버 호출
        LoadFirstMember();
    }

    //teammembers에 있는 첫번째 팀원을 ChoiceManager의 teamMember에 할당
    public void LoadFirstMember()
    {
        ChoiceManager choiceManager = GameObject.Find("ChoiceManager").GetComponent<ChoiceManager>();
        choiceManager.teamMember = teamMembers[0];
    }

}
