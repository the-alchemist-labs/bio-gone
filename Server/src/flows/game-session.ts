import { Server, Socket } from "socket.io";
import { CommandMessageData, SearchMatchData, SocketEvent } from "../types/Sockets";
import { ParseSocketMessage } from "../utils/Parser";

type LobbyRecord = { PlayerId: string, socket: Socket };
const lobby: LobbyRecord[]= [];

export async function searchMatch(data: string, socket: Socket) {
    const { PlayerId } = ParseSocketMessage<SearchMatchData>(data, SearchMatchData);

    OpenRoom([{ socket, PlayerId }]);
    return; // for test
    if (lobby.length > 0) {
        OpenRoom([{ socket, PlayerId }, lobby.shift()!]);
    } else {
        lobby.push({ PlayerId, socket });
    }
}

export async function postCommand(io: Server, commandMessageString: string) {
    const commandMessage = ParseSocketMessage<CommandMessageData>(commandMessageString, CommandMessageData);
    io.to(commandMessage.RoomId).emit(SocketEvent.CommandReceived, commandMessage);

}

function OpenRoom(records: LobbyRecord[]){
    const playerIds = records.map(s => s.PlayerId)
    const roomId = playerIds.join('-');
    records.forEach(s => {
        s.socket.join(roomId);
        s.socket.emit(SocketEvent.MatchFound, { RoomId: roomId, playerIds: playerIds });
    });
}
