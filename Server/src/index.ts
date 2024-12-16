import http from 'http';
import express from 'express';
import bodyParser from 'body-parser';
import { Server } from 'socket.io';
import { initializeSockets } from './sockets/sockets';
import connectDB from './utils/mongodb';
import { config } from './config';
import { errorMiddleware } from './utils/middlewares';
import { players } from './routes/players';
import { lobby } from './routes/lobby';

const app = express();

const server = http.createServer(app);
export const io = new Server(server);

app.use(bodyParser.json());

app.use('/players', players);
app.use('/lobby', lobby);

app.use(errorMiddleware);

server.listen(config.port, async () => {
  await Promise.all([
    connectDB(),
    initializeSockets(io),
  ]);
  console.log(`Server is running on port :${config.port}`);
});