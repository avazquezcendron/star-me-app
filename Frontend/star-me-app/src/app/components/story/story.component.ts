import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MarkdownService } from 'ngx-markdown';
import { StoryService } from '../../services/story.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { EditorInstance, EditorOption } from 'angular-markdown-editor';
import { IBusinessEntity } from '../../models/IBusinessEntity';
import { Story } from '../../models/Story';
import { IResponseValueDTO, IResponseDTO } from '../../contracts/IResponseDTO';
import { ToastrService } from 'ngx-toastr';
import { IResponseMessage } from '../../contracts/IResponseMessage';
import { Store } from '@ngrx/store';
import {
  GlobalStateMode,
  IGlobalState,
} from '../../ngrx/states/globalState.state';
import { ChangeMode } from '../../ngrx/actions/globalState.actions';

@Component({
  selector: 'app-story',
  templateUrl: './story.component.html',
  encapsulation: ViewEncapsulation.None,
  styles: [],
})
export class StoryComponent implements OnInit {
  story: Story;
  dataLoaded = false;
  editMode = false;

  /*
    Markdown Editor
  */
  bsEditorInstance: EditorInstance;
  markdownText: string;
  showEditor = true;
  storyForm: FormGroup;
  editorOptions: EditorOption;
  /*
    Markdown Editor
  */

  constructor(
    public storyService: StoryService,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private markdownService: MarkdownService,
    private router: Router,
    private toastr: ToastrService,
    private globalStore: Store<IGlobalState>
  ) {}

  ngOnInit(): void {
    const storyId = this.route.snapshot.paramMap.get('id');
    if (storyId) {
      this.storyService.get(storyId).subscribe((story: Story) => {
        this.story = story;
        this.dataLoaded = true;
        this.buildForm(this.story.content);
        this.onFormChanges();
      });
    } else {
      this.story = new Story();
      this.globalStore.dispatch(new ChangeMode({ globalMode: GlobalStateMode.NEW }));
      this.editMode = true;
      this.dataLoaded = true;
    }

    this.editorOptions = {
      autofocus: false,
      iconlibrary: 'fa',
      savable: true,
      onFullscreenExit: (e) => this.hidePreview(e),
      onShow: (e) => (this.bsEditorInstance = e),
      parser: (val) => this.parse(val),
    };
  }

  buildForm(markdownText) {
    this.storyForm = this.fb.group({
      body: [markdownText],
      isPreview: [true],
    });
  }

  /** highlight all code found, needs to be wrapped in timer to work properly */
  highlight() {
    setTimeout(() => {
      this.markdownService.highlight();
    });
  }

  hidePreview(e) {
    if (this.bsEditorInstance && this.bsEditorInstance.hidePreview) {
      this.bsEditorInstance.hidePreview();
    }
  }

  showFullScreen(isFullScreen: boolean) {
    if (this.bsEditorInstance && this.bsEditorInstance.setFullscreen) {
      this.bsEditorInstance.showPreview();
      this.bsEditorInstance.setFullscreen(isFullScreen);
    }
  }

  parse(inputValue: string) {
    const markedOutput = this.markdownService.compile(inputValue.trim());
    this.highlight();

    return markedOutput;
  }

  onFormChanges(): void {
    this.storyForm.valueChanges.subscribe((formData) => {
      if (formData) {
        this.story.content = formData.body;
      }
    });
  }

  deleteStory() {
    this.storyService
      .delete(this.story.id)
      .subscribe((response: IResponseDTO) => {
        if (response && response.succeeded) {
          this.globalStore.dispatch(new ChangeMode({ globalMode: GlobalStateMode.BROWSE }));
          this.router.navigate(['/']);
        }
      });
  }
  saveStory() {
    if (this.story.id > 0) {
      this.storyService
        .put(this.story)
        .subscribe((response: IResponseValueDTO) => {
          if (response && response.succeeded) {
            this.globalStore.dispatch(new ChangeMode({ globalMode: GlobalStateMode.BROWSE }));
            window.location.reload();
            // this.router.navigate([`stories/${this.story.id}`]);
          }
        });
    } else {
      this.storyService
        .post(this.story)
        .subscribe((response: IResponseValueDTO) => {
          if (response && response.succeeded) {
            this.globalStore.dispatch(new ChangeMode({ globalMode: GlobalStateMode.BROWSE }));
            this.router.navigate([`stories/${response.data}`]);
          }
        });
    }
  }

  edit() {
    this.globalStore.dispatch(new ChangeMode({ globalMode: GlobalStateMode.EDIT }));
    this.editMode = true;
  }

  discardChanges() {
    this.globalStore.dispatch(new ChangeMode({ globalMode: GlobalStateMode.BROWSE }));
    this.editMode = false;
    if (!this.story.id || this.story.id === 0) {
      this.router.navigate(['/']);
    }
  }
}
