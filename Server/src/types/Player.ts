import { z } from 'zod';

export const playerSchema = z.object({
  id: z.string(),
  name: z.string(),
  profilePicture: z.number(),
});

export type Player = z.infer<typeof playerSchema>;

export const playerSocketConnectionSchema = z.object({
  playerId: z.string(),
});