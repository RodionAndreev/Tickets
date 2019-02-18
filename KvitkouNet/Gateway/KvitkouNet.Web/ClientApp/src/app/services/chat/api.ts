export * from './chat.service';
import { ChatService } from './chat.service';
export * from './room.service';
import { RoomService } from './room.service';
export const APIS = [ChatService, RoomService];
