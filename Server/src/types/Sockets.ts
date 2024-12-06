import { z } from "zod";

export enum SocketEvent {
    Connection = 'connection',
    Disconnect = 'disconnect',
    SearchMatch = 'searchMatch',
    PostCommand = 'postCommand',
    MatchFound = 'matchFound',
    CommandReceived = 'commandReceived',
    Error = 'socketError',
}

export const SearchMatchData = z.object({
    PlayerId: z.string(),
  });
export type SearchMatchData = z.infer<typeof SearchMatchData>;

export const CommandMessageData = z.object({
    RoomId: z.string(),
  }).passthrough();
export type CommandMessageData = z.infer<typeof CommandMessageData>;

export interface CommandMessage  {
    RoomId: string;
    [key: string]: unknown;
  };