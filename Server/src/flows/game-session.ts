import { Server, Socket } from "socket.io";
import { CommandMessageData, SearchMatchData, SocketEvent } from "../types/Sockets";
import { ParseSocketMessage } from "../utils/Parser";

type LobbyRecord = { PlayerId: string, socket: Socket };
const lobby: LobbyRecord[] = [];

export async function searchMatch(data: string, socket: Socket) {
    const { PlayerId } = ParseSocketMessage<SearchMatchData>(data, SearchMatchData);

    //OpenRoom([{ socket, PlayerId }]);
    //return; // for test
    if (lobby.length > 0) {
        OpenRoom([lobby.shift()!, { socket, PlayerId } ]);
    } else {
        lobby.push({ PlayerId, socket });
    }
}

export async function postCommand(io: Server, commandMessageString: string) {
    const commandMessage = ParseSocketMessage<CommandMessageData>(commandMessageString, CommandMessageData);
    io.to(commandMessage.RoomId).emit(SocketEvent.CommandReceived, commandMessage);

}

async function OpenRoom(records: LobbyRecord[]) {
    const playerIds = records.map(s => s.PlayerId);
    const roomId = playerIds.join('-');

    const playersData = await getPlayersData(playerIds);

    records.forEach(s => {
        s.socket.join(roomId);
        s.socket.emit(SocketEvent.MatchFound, {
            RoomId: roomId,
            PlayersData: playersData,
            FirstTurnPlayer: getFirstPlayerIndex(records.length - 1),
        });
    });
}

function getFirstPlayerIndex(playersNum: number) {
    return Math.floor(Math.random() * playersNum)
}

async function getPlayersData(playerIds: string[]) {
    return Promise.resolve([
        {
            PlayerId: playerIds[0],
            Name: 'Sol',
            ProfilePicture: 0,
            Position: 1,
        },
        {
            PlayerId: playerIds[1],
            Name: 'Bla',
            ProfilePicture: 1,
            Position: 4,
        },
    ]);
}