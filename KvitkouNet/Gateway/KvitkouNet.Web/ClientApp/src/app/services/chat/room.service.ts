import { Inject, Injectable, Optional } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams,
         HttpResponse, HttpEvent } from '@angular/common/http';
import { CustomHttpUrlEncodingCodec } from '../../services/chat/encoder';
import { Observable } from 'rxjs';
import { Message } from '../../models/chat/message';
import { Room } from '../../models/chat/room';
import { BASE_PATH, COLLECTION_FORMATS } from '../../services/chat/variables';
import { Configuration } from '../../services/chat/configuration';
import { OAuthService } from 'angular-oauth2-oidc';


@Injectable()
export class RoomService {

    protected basePath = 'https://localhost:5002';  // смотрим на Ocelot
    public defaultHeaders = new HttpHeaders();
    public configuration = new Configuration();

    constructor(protected httpClient: HttpClient, @Optional()@Inject(BASE_PATH) basePath: string,
    @Optional() configuration: Configuration, private oauthService: OAuthService) {
        if (basePath) {
            this.basePath = basePath;
        }
        if (configuration) {
            this.configuration = configuration;
            this.basePath = basePath || configuration.basePath || this.basePath;
        }
    }

    // проверим пользователя
    isAuthenticated() {
      const token = this.oauthService.getAccessToken();
      return !!token;
    }
    // получим Headers
    private getHeaders() {
      const token = this.oauthService.getAccessToken();
      return !!token
        ? new HttpHeaders({
            Authorization: 'Bearer ' + token
          })
        : new HttpHeaders();
    }

    // достаем userId
  public getUserIdFromClaims() {
    const claims = this.oauthService.getIdentityClaims();
    if (claims) {
    return claims['sub'];
  }
  }

    /**
     * @param consumes string[] mime-types
     * @return true: consumes contains 'multipart/form-data', false: otherwise
     */
    private canConsumeForm(consumes: string[]): boolean {
        const form = 'multipart/form-data';
        for (const consume of consumes) {
            if (form === consume) {
                return true;
            }
        }
        return false;
    }


    /**
     *
     *
     * @param message
     * @param rid
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public roomAddMessage(message: Message, rid: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public roomAddMessage(message: Message, rid: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public roomAddMessage(message: Message, rid: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public roomAddMessage(message: Message, rid: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (message === null || message === undefined) {
            throw new Error('Required parameter message was null or undefined when calling roomAddMessage.');
        }

        if (rid === null || rid === undefined) {
            throw new Error('Required parameter rid was null or undefined when calling roomAddMessage.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.post<any>(`${this.basePath}/chat/rooms/${encodeURIComponent(String(rid))}/message`,
            message,
            {
                withCredentials: this.configuration.withCredentials,
                headers: this.getHeaders(),
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     *
     *
     * @param room
     * @param uid
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public roomAddRoom(room: Room, uid: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public roomAddRoom(room: Room, uid: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public roomAddRoom(room: Room, uid: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public roomAddRoom(room: Room, uid: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (room === null || room === undefined) {
            throw new Error('Required parameter room was null or undefined when calling roomAddRoom.');
        }

        if (uid === null || uid === undefined) {
            throw new Error('Required parameter uid was null or undefined when calling roomAddRoom.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.post<any>(`${this.basePath}/chat/rooms/room/${encodeURIComponent(String(uid))}`,
            room,
            {
                withCredentials: this.configuration.withCredentials,
                headers: this.getHeaders(),
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     *
     *
     * @param mid
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public roomDeleteMessage(mid: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public roomDeleteMessage(mid: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public roomDeleteMessage(mid: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public roomDeleteMessage(mid: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (mid === null || mid === undefined) {
            throw new Error('Required parameter mid was null or undefined when calling roomDeleteMessage.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];

        return this.httpClient.delete<any>(`${this.basePath}/chat/rooms/messages/${encodeURIComponent(String(mid))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: this.getHeaders(),
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     *
     *
     * @param message
     * @param rid
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public roomEditMessage(message: Message, rid: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public roomEditMessage(message: Message, rid: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public roomEditMessage(message: Message, rid: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public roomEditMessage(message: Message, rid: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (message === null || message === undefined) {
            throw new Error('Required parameter message was null or undefined when calling roomEditMessage.');
        }

        if (rid === null || rid === undefined) {
            throw new Error('Required parameter rid was null or undefined when calling roomEditMessage.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.patch<any>(`${this.basePath}/chat/rooms/${encodeURIComponent(String(rid))}/message`,
            message,
            {
                withCredentials: this.configuration.withCredentials,
                headers: this.getHeaders(),
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     *
     *
     * @param rid
     * @param historyCountsMessages
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public roomGetMessages(rid: string, historyCountsMessages: number, observe?: 'body', reportProgress?: boolean): Observable<Array<Message>>;
    public roomGetMessages(rid: string, historyCountsMessages: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<Message>>>;
    public roomGetMessages(rid: string, historyCountsMessages: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<Message>>>;
    public roomGetMessages(rid: string, historyCountsMessages: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (rid === null || rid === undefined) {
            throw new Error('Required parameter rid was null or undefined when calling roomGetMessages.');
        }

        if (historyCountsMessages === null || historyCountsMessages === undefined) {
            throw new Error('Required parameter historyCountsMessages was null or undefined when calling roomGetMessages.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];

        return this.httpClient.get<Array<Message>>(`${this.basePath}/chat/rooms/${encodeURIComponent(String(rid))}/messages/history/${encodeURIComponent(String(historyCountsMessages))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: this.getHeaders(),
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     *
     *
     * @param uid
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public roomGetRooms(uid: string, observe?: 'body', reportProgress?: boolean): Observable<Array<Room>>;
    public roomGetRooms(uid: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<Room>>>;
    public roomGetRooms(uid: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<Room>>>;
    public roomGetRooms(uid: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (uid === null || uid === undefined) {
            throw new Error('Required parameter uid was null or undefined when calling roomGetRooms.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];

        return this.httpClient.get<Array<Room>>(`${this.basePath}/chat/rooms/users/${encodeURIComponent(String(uid))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: this.getHeaders(),
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     *
     *
     * @param rid
     * @param template
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public roomSearchMessage(rid: string, template: string, observe?: 'body', reportProgress?: boolean): Observable<Array<Message>>;
    public roomSearchMessage(rid: string, template: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<Message>>>;
    public roomSearchMessage(rid: string, template: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<Message>>>;
    public roomSearchMessage(rid: string, template: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (rid === null || rid === undefined) {
            throw new Error('Required parameter rid was null or undefined when calling roomSearchMessage.');
        }

        if (template === null || template === undefined) {
            throw new Error('Required parameter template was null or undefined when calling roomSearchMessage.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];

        return this.httpClient.get<Array<Message>>(`${this.basePath}/chat/rooms/${encodeURIComponent(String(rid))}/messages/${encodeURIComponent(String(template))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: this.getHeaders(),
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     *
     *
     * @param template
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public roomSearchRoom(template: string, observe?: 'body', reportProgress?: boolean): Observable<Array<Room>>;
    public roomSearchRoom(template: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<Room>>>;
    public roomSearchRoom(template: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<Room>>>;
    public roomSearchRoom(template: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (template === null || template === undefined) {
            throw new Error('Required parameter template was null or undefined when calling roomSearchRoom.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];

        return this.httpClient.get<Array<Room>>(`${this.basePath}/chat/rooms/${encodeURIComponent(String(template))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: this.getHeaders(),
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

}
