export enum SocketEvent {
    Connection = 'connection',
    Disconnect = 'disconnect',
    SearchMatch = 'searchMatch',
    PostCommand = 'postCommand',
    MatchFound = 'matchFound',
    CommandReceived = 'commandReceived',
    Error = 'socketError',
}

export interface SearchMatchData {
    playerId: string;
}

export interface CommandMessage  {
    roomId: string;
    [key: string]: unknown;
  };