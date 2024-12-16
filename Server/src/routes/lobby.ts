import { Router, Request, Response } from 'express'; 
import * as gameSession from '../flows/game-session';
import { asyncMiddleware } from '../utils/middlewares';

const lobby = Router();

async function removePlayerFromLobby(req: Request, res: Response) {
  await gameSession.removePlayerFromLobby(req.params.playerId);
  return res.send();
}

lobby.delete('/remove/:playerId', asyncMiddleware(removePlayerFromLobby));

export { lobby };
