import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { User } from '../_models';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    constructor(private http: HttpClient) { }

    private apiUrl = 'https://localhost:44324/api';  // URL to web api

    register(user: User) {
        return this.http.post(`${this.apiUrl}/user/register`, user);
    }
}