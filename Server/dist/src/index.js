"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.io = void 0;
const http_1 = __importDefault(require("http"));
const express_1 = __importDefault(require("express"));
const body_parser_1 = __importDefault(require("body-parser"));
const socket_io_1 = require("socket.io");
const sockets_1 = require("./sockets/sockets");
const mongodb_1 = __importDefault(require("./utils/mongodb"));
const config_1 = require("./config");
const middlewares_1 = require("./utils/middlewares");
const players_1 = require("./routes/players");
const lobby_1 = require("./routes/lobby");
const app = (0, express_1.default)();
const server = http_1.default.createServer(app);
exports.io = new socket_io_1.Server(server, {
    cors: {
        origin: '*', // Allow all origins
        methods: ['GET', 'POST'],
        allowedHeaders: ['Content-Type', 'Authorization'],
        credentials: true,
    },
});
app.use(body_parser_1.default.json());
app.use('/players', players_1.players);
app.use('/lobby', lobby_1.lobby);
app.use(middlewares_1.errorMiddleware);
server.listen(config_1.config.port, () => __awaiter(void 0, void 0, void 0, function* () {
    yield Promise.all([
        (0, mongodb_1.default)(),
        (0, sockets_1.initializeSockets)(exports.io),
    ]);
    console.log(`Server is running on port :${config_1.config.port}`);
}));
//# sourceMappingURL=index.js.map