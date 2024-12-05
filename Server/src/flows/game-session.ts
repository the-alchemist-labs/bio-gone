import { Server, Socket } from "socket.io";
import { CommandMessage, SearchMatchData, SocketEvent } from "../types/Sockets";

type LobbyRecord = { playerId: string, socket: Socket };
const lobby: LobbyRecord[]= [];

export async function searchMatch({ playerId }: SearchMatchData, socket: Socket) {
    OpenRoom([socket]);
    return; // for test
    if (lobby.length > 0 || true) {
        OpenRoom([socket, lobby.shift()!.socket]);
    } else {
        lobby.push({ playerId, socket});
    }
}

export async function postCommand(io: Server, commandMessage: CommandMessage) {
    io.to(commandMessage.roomId).emit(SocketEvent.CommandReceived, commandMessage);
}

function OpenRoom(sockets: Socket[]){
    const roomId = crypto.randomUUID();
    sockets.forEach(s => {
        s.join(roomId);
        s.emit(SocketEvent.MatchFound, { roomId });
    });
}
