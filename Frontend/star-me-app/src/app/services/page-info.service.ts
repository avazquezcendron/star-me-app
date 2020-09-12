import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IPageInfo } from '../components/shared/interfaces/PageInfo/IPageInfo';

@Injectable({
  providedIn: 'root',
})
export class PageInfoService {
  private data: IPageInfo;
  private dataLoaded = false;

  public get GetPageData(): IPageInfo {
    return this.data;
  }

  public get DataLoaded(): boolean{
    return this.dataLoaded;
  }

  constructor(private http: HttpClient) {
    // const url = 'https://osi-web-site.firebaseio.com/pageInfo.json';
    // this.http.get<IPageInfo>(url).subscribe((jsonData) => {
    //   this.data = jsonData;
    //   this.dataLoaded = true;
    // });
    this.dataLoaded = true;
  }
}
