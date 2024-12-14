import * as PlayersStore from '../stores/PlayerStore';
import { Player } from '../types/Player';
import { generateCodeFromUniqueId } from "../utils/CodeGenerator";

export async function getPlayer(playerId: string): Promise<Player | null> {
    const player = await PlayersStore.GetPlayerById(playerId);
    if (player) return player;

    const newPlayer = await PlayersStore.CreatePlayer({
        id: playerId,
        name: `Player_${generateCodeFromUniqueId(playerId)}`,
        profilePicture: 0,
    });

    console.log("Player registered", playerId);
    return newPlayer;
}
