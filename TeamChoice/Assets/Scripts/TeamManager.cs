using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeamManager : MonoBehaviour
{
    // 팀장 정보를 담는 클래스
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
    // 팀원 정보를 담는 클래스
    [System.Serializable]
    public class TeamMember
    {
        public string name;
        public int order;
        public string imageName;

        public TeamMember(string name, int order, string imageName)
        {
            this.name = name;
            this.order = order;
            this.imageName = imageName;
        }
    }
    // 팀 정보를 담는 클래스
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

    public List<TeamLeader> teamLeaders;
    public List<TeamMember> teamMembers;
    public List<TeamMember> kickMembers;
    public List<TeamMember> waitingTeamMembers;
    public List<Image> leaderImages;
    public List<TMP_Text> leaderNames;
    public List<Team> teams;
    public List<List<Image>> teamMemberImages;
    public List<List<TMP_Text>> teamMemberNames;
    public GameObject kickPanel;
    public List<Image> kickMemberImages;
    public List<TMP_Text> kickMemberNames;

    private const int MaxMembersPerTeam = 2;

    public void Start()
    {
        // 팀장, 팀원, 팀 정보 초기화
        InitializeTeams();
        InitializeUI();
        LoadFirstMember();
    }

    private void InitializeTeams()
    {
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

        teamMembers = new List<TeamMember>
        {
        new TeamMember("팀원1", 0, "팀원1"),
        new TeamMember("팀원2", 1, "팀원2"),
        new TeamMember("팀원3", 2, "팀원3"),
        new TeamMember("팀원4", 3, "팀원4"),
        new TeamMember("팀원5", 4, "팀원5"),
        new TeamMember("팀원6", 5, "팀원6"),
        new TeamMember("팀원7", 6, "팀원7"),
        new TeamMember("팀원8", 7, "팀원8"),
        new TeamMember("팀원9", 8, "팀원9"),
        new TeamMember("팀원10", 9, "팀원10"),
        new TeamMember("팀원11", 10, "팀원11"),
        new TeamMember("팀원12", 11, "팀원12"),
        new TeamMember("팀원13", 12, "팀원13"),
        new TeamMember("팀원14", 13, "팀원14"),
        new TeamMember("팀원15", 14, "팀원15"),
        new TeamMember("팀원16", 15, "팀원16"),
        new TeamMember("팀원17", 16, "팀원17"),
        new TeamMember("팀원18", 17, "팀원18"),
        new TeamMember("팀원19", 18, "팀원19"),
        new TeamMember("팀원20", 19, "팀원20")
        };

        teams = new List<Team>();
        for (int i = 0; i < teamLeaders.Count; i++)
        {
            teams.Add(new Team(new List<TeamLeader> { teamLeaders[i] }, new List<TeamMember>()));
        }
    }

    private void InitializeUI()
    {
        for (int i = 0; i < leaderNames.Count && i < teamLeaders.Count; i++)
        {
            leaderNames[i].text = teamLeaders[i].name;
        }

        for (int i = 0; i < leaderImages.Count && i < teamLeaders.Count; i++)
        {
            leaderImages[i].sprite = Resources.Load<Sprite>("Images/TeamLeader/" + teamLeaders[i].name);
        }

        teamMemberImages = new List<List<Image>>();
        for (int i = 0; i < teams.Count; i++)
        {
            teamMemberImages.Add(new List<Image>());
            for (int j = 0; j < MaxMembersPerTeam; j++)
            {
                teamMemberImages[i].Add(GameObject.Find("Team" + i + "Member" + j + "Image").GetComponent<Image>());
            }
        }

        teamMemberNames = new List<List<TMP_Text>>();
        for (int i = 0; i < teams.Count; i++)
        {
            teamMemberNames.Add(new List<TMP_Text>());
            for (int j = 0; j < MaxMembersPerTeam; j++)
            {
                teamMemberNames[i].Add(GameObject.Find("Team" + i + "Member" + j + "Name").GetComponent<TMP_Text>());
            }
        }
    }

    public void LoadFirstMember()
    {
        // 첫번째 멤버를 불러오고 초기화
        ChoiceManager choiceManager = GameObject.Find("ChoiceManager").GetComponent<ChoiceManager>();
        choiceManager.currentTeamMember = teamMembers[0];
        Debug.Log(choiceManager.currentTeamMember.name);
        Debug.Log(choiceManager.currentTeamMember.order);
    }

    public void LoadNextMember(int index)
    {
        // 다음 멤버를 불러오고 초기화
        ChoiceManager choiceManager = GameObject.Find("ChoiceManager").GetComponent<ChoiceManager>();
        choiceManager.currentTeamMember = teamMembers[index];
        Debug.Log(choiceManager.currentTeamMember.name);
        Debug.Log(choiceManager.currentTeamMember.order);
    }

    public void AddMemberToTeam(int teamIndex)
    {
        // 팀에 멤버 추가
        ChoiceManager choiceManager = GameObject.Find("ChoiceManager").GetComponent<ChoiceManager>();
        Team team = teams.FirstOrDefault(t => t.leader[0].order == teamIndex);

        if (team != null && team.members.Count < MaxMembersPerTeam)
        {
            team.members.Add(choiceManager.currentTeamMember);
            UpdateTeamUI(teamIndex, team.members.Count - 1, choiceManager.currentTeamMember);
            choiceManager.LoadNextMember();
        }
        else if (team != null && team.members.Count == MaxMembersPerTeam)
        {
            choiceManager.currentTeamIndex = teamIndex;
            OpenKickPanel(teamIndex);
        }
    }

    public void OpenKickPanel(int teamIndex)
    {
        kickPanel.SetActive(true);
        // 현재 선택한 팀 호출
        ChoiceManager choiceManager = GameObject.Find("ChoiceManager").GetComponent<ChoiceManager>();
        Team team = teams.FirstOrDefault(t => t.leader[0].order == teamIndex);
        // kickMemberNames에 teamindex에 해당하는 팀원 이름과 choiceManager의 currentTeamMember 이름을 넣어줌
        kickMemberNames[0].text = team.members[0].name;
        kickMemberNames[1].text = team.members[1].name;
        kickMemberNames[2].text = choiceManager.currentTeamMember.name;
        // kickMemberImages에 teamindex에 해당하는 팀원 이미지와 choiceManager의 currentTeamMember 이미지를 넣어줌
        kickMemberImages[0].sprite = Resources.Load<Sprite>("Images/TeamMember/" + team.members[0].imageName);
        kickMemberImages[1].sprite = Resources.Load<Sprite>("Images/TeamMember/" + team.members[1].imageName);
        kickMemberImages[2].sprite = Resources.Load<Sprite>("Images/TeamMember/" + choiceManager.currentTeamMember.imageName);
        // kickMembers에 teamindex에 해당하는 팀원과 chiocemanager의 currentTeamMember를 넣어줌
        kickMembers = new List<TeamMember> { team.members[0], team.members[1], choiceManager.currentTeamMember };
    }

    // kickmembers 리스트에 있는 지정한 멤버를 waitingMembers 리스트로 이동시키고 나머지 팀원은 기존에 있던 팀 리스트로 이동
    public void KickMemberFromTeam(int memberIndex)
    {
        // kickMembers 리스트에 있는 팀원을 teams 리스트에서 삭제
        ChoiceManager choiceManager = GameObject.Find("ChoiceManager").GetComponent<ChoiceManager>();
        Team team = teams.FirstOrDefault(t => t.leader[0].order == choiceManager.currentTeamIndex);
        team.members.Clear();
        // waitingTeamMembers 리스트에 kickmembers 리스트에서 memberIndex 차례에 해당하는 팀원을 추가
        waitingTeamMembers.Add(kickMembers[memberIndex]);
        // kickMembers 리스트에서 memberIndex 차례에 해당하는 팀원을 삭제
        kickMembers.RemoveAt(memberIndex);
        // kickMembers 리스트 정렬
        kickMembers.Sort((a, b) => a.order.CompareTo(b.order));
        // kickMemers 리스트에 있는 팀원을 teams 리스트에 추가
        for (int i = 0; i < kickMembers.Count; i++)
        {
            team.members.Add(kickMembers[i]);
            UpdateTeamUI(choiceManager.currentTeamIndex, i, kickMembers[i]);
        }
        // kickMembers 리스트 초기화
        kickMembers.Clear();
        // kickPanel 비활성화
        kickPanel.SetActive(false);
        // 팀원 UI 업데이트
        for (int i = 0; i < team.members.Count; i++)
        {
            UpdateTeamUI(choiceManager.currentTeamIndex, i, team.members[i]);
        }
        choiceManager.LoadNextMember();
        // choiceManager의 currentTeamIndex 초기화
        choiceManager.currentTeamIndex = 10;
    }


    private void UpdateTeamUI(int teamIndex, int memberIndex, TeamMember member)
    {
        string imagePath = "Images/TeamMember/" + member.imageName;
        Sprite memberSprite = Resources.Load<Sprite>(imagePath);
        if (memberSprite == null)
        {
            Debug.LogError("Failed to load sprite at path: " + imagePath);
            return;
        }
        Debug.Log(teamIndex + memberIndex);
        teamMemberImages[teamIndex][memberIndex].sprite = memberSprite;
        teamMemberNames[teamIndex][memberIndex].text = member.name;
    }
}