import mongoose, { Schema } from 'mongoose';
import { Player } from '../types/Player';

const PlayerSchema: Schema<Player> = new Schema({
  id: { type: String, required: true, unique: true },
  name: { type: String, required: true },
  profilePicture: { type: Number, required: true },
});

export const PlayerModel = mongoose.model<Player>('Player', PlayerSchema);