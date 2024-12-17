"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.asyncMiddleware = asyncMiddleware;
exports.errorMiddleware = errorMiddleware;
exports.socketHandler = socketHandler;
const Sockets_1 = require("../types/Sockets");
function asyncMiddleware(fn) {
    return (req, res, next) => {
        Promise.resolve(fn(req, res, next))
            .catch(next);
    };
}
function errorMiddleware(error, _req, res, _next) {
    console.error(error.stack);
    res.status(error.status || 500).json({
        message: error.message || 'Internal Server Error',
        error,
    });
}
;
function socketHandler(socket, event, handler) {
    socket.on(event, (message) => {
        handler(message).catch((err) => {
            socket.emit(Sockets_1.SocketEvent.Error, { message: err.message });
            console.error(`Error in handler for ${event}:`, err.message);
        });
    });
}
//# sourceMappingURL=middlewares.js.map