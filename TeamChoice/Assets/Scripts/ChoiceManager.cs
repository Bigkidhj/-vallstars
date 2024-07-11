using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    public TeamManager teamManager;
    public TeamManager.TeamMember currentTeamMember;
    public int currentMemberIndex = 0;
    public int currentTeamIndex = 10;
    public void Start()
    {
        //TeamManager를 찾아서 teamManager에 할당
        teamManager = GameObject.Find("TeamManager").GetComponent<TeamManager>();
    }

    public void LoadNextMember()
    {
        //팀원 리스트가 남아있다면 다음 멤버 호출
        if(currentMemberIndex < 19)
        {
            currentMemberIndex++;
            teamManager.LoadNextMember(currentMemberIndex);
        } else
        {
            Debug.Log("더 이상 팀원이 없습니다.");
        }
    }

    // 현재 teamMember를 teamManager의 특정 Team 안에 있는 teamMembers 리스트에 추가
    public void AddMemberToTeam(int TeamIndex)
    {
        if(currentMemberIndex <= 19)
        {
            teamManager.AddMemberToTeam(TeamIndex);
        }
        else
        {
            Debug.Log("더 이상 팀원이 없습니다. 멤버를 추가할 수 없습니다.");
        }
    }

    // teamManager의 kickmembers 리스트에 있는 특정 멤버를 waitingMembers 리스트로 이동시키고 나머지 팀원은 기존에 있던 팀 리스트로 이동
    public void KickMemberFromTeam(int memberIndex)
    {
        if (currentMemberIndex <= 19)
        {
            teamManager.KickMemberFromTeam(memberIndex);
        }
        else
        {
            Debug.Log("더 이상 팀원이 없습니다. 멤버를 내보낼 수 없습니다.");
        }
    }
}
