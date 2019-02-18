import { Room } from './../../models/chat/room';
import { Settings } from './../../models/chat/settings';
import { Observable } from 'rxjs';
import { Message } from './../../models/chat/message';
import { RoomService } from './../../services/chat/room.service';
import { ChatService } from './../../services/chat/chat.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { stringify } from '@angular/compiler/src/util';


@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  chatForm: FormGroup;
  messagesOnTemplate: Message[] = []; // результат найденых сообщений по шаблону
  templateMessage: string;  // шаблон сообщения для поиска
  userSettins: Settings;
  private connection: HubConnection;
  public messagesForHub: Array<Message> = []; // сообщения переданные на форму через Hub
  authenticated: boolean;
  roomCreated: string;
  messageNotExist: string;

  constructor(
    private serviceChat: ChatService, private serviceRoom: RoomService
    ) {
      // прошел ли пользователь Authenticat
      this.authenticated = this.serviceChat.isAuthenticated();
      console.log('настраиваем коннект для Hub');
      // настроим коннект для Hub
      this.connection = new HubConnectionBuilder()
      .withUrl('https://localhost:5002/chat/notification')  // смотрим на Ocelot
      .build();
      console.log('стартуем коннект для Hub');
      this.connection
      .start()
      .then(() => console.log('Connection established'))
      .catch(err => console.error('ошибка коннекта'));

    // регистрируемся на метод alertOnSendedMessageAllUsers
    this.connection.on('alertOnSendedMessageAllUsers', msg =>
    (console.log('startMethodHub. Came in method  = ' + msg ),
      this.messagesForHub.push(msg),
      console.log('EndMethodHub')
      ));
     }

  ngOnInit() {
}

// отправка сообщения
  onAddMessage(textMessage: string) {

// формируем сообщение
     const message: Message = {
      text: textMessage,
      sendedTime: new Date(),
      isEdit: false,
      userId: this.serviceChat.getUserIdFromClaims() // получаем UserId если не работает нужно указать - '1'
    };
    // '1' - это номер комнаты(на данный момент будет только одна комната)
     this.serviceRoom.roomAddMessage(message, '1').subscribe(
       (r) => console.log('сообщение успешно отправлено')
     , err => console.log('err'));
  }

  // получим пользовательские настройки для чата
  onGetUserSetting() {

    // если не будет работать нужно указать UserId = 1
     this.serviceChat.chatGetUserSettings(this.serviceChat.getUserIdFromClaims() ).subscribe(x => {
        this.userSettins = x;
      }
  );
  }

  // выполним поиск сообщения по шаблону
  onSearchMessage(templateMessageIn: string) {
    this.templateMessage = templateMessageIn;

        // '1' - это номер комнаты(на данный момент будет только одна комната)
    this.serviceRoom.roomSearchMessage('1', this.templateMessage).subscribe(x => {
console.log('пришло ', x);
      if (x !== null) {
        this.messagesOnTemplate = x;
      } else { this.messageNotExist = 'Сообщение не найдено!'; }

      });
  }

  // создадим комнату
  onCreateRoom() {
    // формируем Main комнату
    const mainRoom: Room = {
      id: '1',  // у главной комнаты будет id =1
      name: 'MainRoom',
      isPrivat: false
    };
// если не будет работать нужно указать UserId = 1
    this.serviceRoom.roomAddRoom(mainRoom, this.serviceChat.getUserIdFromClaims() ).subscribe(x => {
      this.roomCreated = '!!!КОМНАТА УЖЕ СОЗДАНА!!!';
    });
      // r => console.log('Комната успешно создана')
      // ,err => console.log('Комната не создана'));
  }
}
