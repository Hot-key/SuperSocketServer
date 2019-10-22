namespace _2.SuperSockeChatClient
{
    public enum ChatProtocol : ushort
    {
        RoomConnection = 0x1001,  // 방 연결시
        RoomDisconnection = 0x1002, // 방 접속종료

        RoomUserList = 0x2001, // 모든 유저 정보
        RoomUserCount = 0x2002, // 방의 유저 수
        RoomUserInfo = 0x2003, // 특정 유저의 정보

        UserNameChange = 0x3001, // 이름변경

        RoomSendMessage = 0x4001, // 메시지 전송
        RoomRemoveMessage = 0x4002, // 메시지 지우기
        RoomEditMessage = 0x4003, // 메시지 수정
    }
}