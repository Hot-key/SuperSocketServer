using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.SuperSockeChat
{
    public enum ChatProtocol : ushort
    {
        RoomCreate = 0x0001, // 방 만들기
        RoomRemove = 0x0002, // 방 지우기

        RoomConnection = 0x1001, // 방 연결시
        RoomDisconnection = 0x1002, // 방 접속종료

        RoomUserList = 0x2001, // 모든 유저 정보
        RoomUserCount = 0x2002, // 방의 유저 수
        RoomUserInfo = 0x2003, // 특정 유저의 정보

        UseRegister = 0x3001, // 회원가입
        UserLogin = 0x3002, // 로그인

        UserNameChange = 0x3011, // 이름변경

        RoomSendMessage = 0x4001, // 메시지 전송
        RoomRemoveMessage = 0x4002, // 메시지 지우기
        RoomEditMessage = 0x4003, // 메시지 수정
    }
}
