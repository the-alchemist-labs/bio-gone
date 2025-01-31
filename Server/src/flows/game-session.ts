import { Server, Socket } from "socket.io";
import { CommandMessageData, SearchMatchData, SocketEvent } from "../types/Sockets";
import * as playersFlow from "../flows/players";

type LobbyRecord = { PlayerId: string, socket: Socket };
const lobby: LobbyRecord[] = [];

export async function searchMatch({ PlayerId }: SearchMatchData, socket: Socket) {
    if (lobby.length > 0) {
        OpenRoom([lobby.shift()!, { socket, PlayerId }]);
    } else {
        lobby.push({ PlayerId, socket });
        console.log(`Joined lobby - ${PlayerId}`);
    }
}

export async function postCommand(io: Server, commandMessage: CommandMessageData) {
    io.to(commandMessage.RoomId).emit(SocketEvent.CommandReceived, commandMessage);
}

export async function removePlayerFromLobby(identifier: string) {
    const index = lobby.findIndex(record => record.PlayerId === identifier || record.socket.id === identifier);
    if (index !== -1) {
        lobby.splice(index, 1);
        console.log(`Removed ${identifier} from the lobby`);
    }
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
    const players = await Promise.all(playerIds.map(playersFlow.getPlayer));
    return players.map(p => ({
        Id: p?.id,
        Name: p?.name,
        ProfilePicture: p?.profilePicture,
    }));
}