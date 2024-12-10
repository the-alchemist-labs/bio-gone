import { Request, Response, NextFunction, RequestHandler } from 'express';
import { SocketEvent } from '../types/Sockets';
import { Socket } from 'socket.io';

export function asyncMiddleware(fn: RequestHandler) {
    return (req: Request, res: Response, next: NextFunction) => {
        Promise.resolve(fn(req, res, next))
            .catch(next)
    }
}

export function errorMiddleware(error: any, _req: Request, res: Response, _next: NextFunction) {
    console.error(error.stack);

    res.status(error.status || 500).json({
        message: error.message || 'Internal Server Error',
        error,
    });
};

export function socketHandler(socket: Socket, event: SocketEvent, handler: (message: string) => Promise<void>) {
    socket.on(event, (message: string) => {
        handler(message).catch((err) => {
            socket.emit(SocketEvent.Error, { message: err.message });
            console.error(`Error in handler for ${event}:`, err.message);
        });
    });
}
