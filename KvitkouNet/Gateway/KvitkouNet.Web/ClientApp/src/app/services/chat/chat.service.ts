import { Inject, Injectable, Optional } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams,
         HttpResponse, HttpEvent } from '@angular/common/http';
import { CustomHttpUrlEncodingCodec } from '../../services/chat/encoder';
import { Observable } from 'rxjs';
import { Settings } from '../../models/chat/settings';
import { BASE_PATH, COLLECTION_FORMATS } from '../../services/chat/variables';
import { Configuration } from '../../services/chat/configuration';
import { OAuthService } from 'angular-oauth2-oidc';


@Injectable()
export class ChatService {

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
     * @param uid
     * @param settings
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public chatEditUserSettings(uid: string, settings: Settings, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public chatEditUserSettings(uid: string, settings: Settings, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public chatEditUserSettings(uid: string, settings: Settings, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public chatEditUserSettings(uid: string, settings: Settings, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (uid === null || uid === undefined) {
            throw new Error('Required parameter uid was null or undefined when calling chatEditUserSettings.');
        }

        if (settings === null || settings === undefined) {
            throw new Error('Required parameter settings was null or undefined when calling chatEditUserSettings.');
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

        return this.httpClient.patch<any>(`${this.basePath}/chat/settings/${encodeURIComponent(String(uid))}`,
            settings,
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
    public chatGetUserSettings(uid: string, observe?: 'body', reportProgress?: boolean): Observable<Settings>;
    public chatGetUserSettings(uid: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Settings>>;
    public chatGetUserSettings(uid: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Settings>>;
    public chatGetUserSettings(uid: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (uid === null || uid === undefined) {
            throw new Error('Required parameter uid was null or undefined when calling chatGetUserSettings.');
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

        return this.httpClient.get<Settings>(`${this.basePath}/chat/settings/${encodeURIComponent(String(uid))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: this.getHeaders(),
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

}
