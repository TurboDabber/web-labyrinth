import { Injectable, Optional, InjectionToken, Inject } from '@angular/core';
import { ApiClient } from './api/services.generated';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private readonly apiClient: ApiClient;

  constructor(@Optional() @Inject(API_BASE_URL) baseUrl?: string) {
    this.apiClient = new ApiClient(baseUrl);
  }

  get client(): ApiClient {
    return this.apiClient;
  }
}