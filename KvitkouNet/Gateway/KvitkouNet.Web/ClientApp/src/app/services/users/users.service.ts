import { Registration } from '../../models/registration';
import { Users } from '../../models/users';
import { Inject, Injectable, Optional } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams,
         HttpResponse, HttpEvent } from '@angular/common/http';
import { CustomHttpUrlEncodingCodec } from './encoder';

import { Observable } from 'rxjs';

import { EmailUpdateMessage } from '../../models/users/emailUpdateMessage';
import { ForUpdateModel } from '../../models/users/forUpdateModel';
import { ForViewModel } from '../../models/users/forViewModel';
import { UserRegisterModel } from '../../models/users/userRegisterModel';

import { BASE_PATH, COLLECTION_FORMATS } from './variables';
import { Configuration } from './configuration';


@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private basePath = 'http://localhost:5003';
  public defaultHeaders = new HttpHeaders();
    public configuration = new Configuration();

    constructor(private httpClient: HttpClient, @Optional()@Inject(BASE_PATH) basePath: string,
    @Optional() configuration: Configuration) {
        if (basePath) {
            this.basePath = basePath;
        }
        if (configuration) {
            this.configuration = configuration;
            this.basePath = basePath || configuration.basePath || this.basePath;
        }
    }

    private canConsumeForm(consumes: string[]): boolean {
      const form = 'multipart/form-data';
      for (const consume of consumes) {
          if (form === consume) {
              return true;
          }
      }
      return false;
  }

  getUsers() {
    return this.httpClient.get<Users[]>(`${this.basePath}/api/users`);
  }
  sendUser(body) {
    return this.httpClient.post(`${this.basePath}/api/users/register`, body);
  }


  /*
      @param id
      @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
      @param reportProgress flag to report request and response progress.
     */
    public userDelete(id: string, observe?: 'body', reportProgress?: boolean): Observable<string>;
    public userDelete(id: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<string>>;
    public userDelete(id: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<string>>;
    public userDelete(id: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling userDelete.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        const httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];

        return this.httpClient.delete<string>(`${this.basePath}/api/users/${encodeURIComponent(String(id))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /*
      @param id
      @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
      @param reportProgress flag to report request and response progress.
     */
    public userGet(id: string, observe?: 'body', reportProgress?: boolean): Observable<Array<ForViewModel>>;
    public userGet(id: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<ForViewModel>>>;
    public userGet(id: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<ForViewModel>>>;
    public userGet(id: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling userGet.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        const httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];

        return this.httpClient.get<Array<ForViewModel>>(`${this.basePath}/api/users/${encodeURIComponent(String(id))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     *
     *
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public userGetAll(observe?: 'body', reportProgress?: boolean): Observable<Array<ForViewModel>>;
    public userGetAll(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<ForViewModel>>>;
    public userGetAll(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<ForViewModel>>>;
    public userGetAll(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let headers = this.defaultHeaders;

        // to determine the Accept header
        const httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];

        return this.httpClient.get<Array<ForViewModel>>(`${this.basePath}/api/users`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /*
      @param model
      @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
      @param reportProgress flag to report request and response progress.
     */
    public userRegister(model: UserRegisterModel, observe?: 'body', reportProgress?: boolean): Observable<string>;
    public userRegister(model: UserRegisterModel, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<string>>;
    public userRegister(model: UserRegisterModel, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<string>>;
    public userRegister(model: UserRegisterModel, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (model === null || model === undefined) {
            throw new Error('Required parameter model was null or undefined when calling userRegister.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        const httpHeaderAccepts: string[] = [
        ];
        let httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected = undefined) {
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
        if (httpContentTypeSelected !== undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.post<string>(`${this.basePath}/api/users/register`,
            model,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /*
     * @param id
     * @param userModel
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public userUpdate(id: string, userModel: ForUpdateModel, observe?: 'body', reportProgress?: boolean): Observable<string>;
    public userUpdate(id: string, userModel: ForUpdateModel, observe?: 'response', reportProgress?: boolean)
    : Observable<HttpResponse<string>>;
    public userUpdate(id: string, userModel: ForUpdateModel, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<string>>;
    public userUpdate(id: string, userModel: ForUpdateModel, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling userUpdate.');
        }

        if (userModel === null || userModel === undefined) {
            throw new Error('Required parameter userModel was null or undefined when calling userUpdate.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        const httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
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
        if (httpContentTypeSelected !== undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.put<string>(`${this.basePath}/api/users/${encodeURIComponent(String(id))}`,
            userModel,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /*
     * @param emailUpdateMessage
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public userUpdateEmail(emailUpdateMessage: EmailUpdateMessage, observe?: 'body', reportProgress?: boolean): Observable<boolean>;
    public userUpdateEmail(emailUpdateMessage:
      EmailUpdateMessage, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<boolean>>;
    public userUpdateEmail(emailUpdateMessage:
      EmailUpdateMessage, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<boolean>>;
    public userUpdateEmail(emailUpdateMessage:
      EmailUpdateMessage, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (emailUpdateMessage === null || emailUpdateMessage === undefined) {
            throw new Error('Required parameter emailUpdateMessage was null or undefined when calling userUpdateEmail.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        const httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
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
        if (httpContentTypeSelected !== undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.get<boolean>(`${this.basePath}/api/users/email`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }
}
