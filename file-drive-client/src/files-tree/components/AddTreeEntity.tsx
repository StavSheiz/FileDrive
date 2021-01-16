import { Card, IconButton, TextField } from '@material-ui/core'
import { AttachFile, CreateNewFolder } from '@material-ui/icons'
import React, { useState } from 'react'
interface IAddTreeEntityProps {
    onAddFile: (file: File) => void,
    onAddFolder: (folderName: string) => void
}

const AddTreeEntity = (props: IAddTreeEntityProps) => {
    const { onAddFile, onAddFolder } = props

    const [isAddFolderMode, setIsAddFolderMode] = useState<boolean>(false)

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.files && e.target.files[0]) {
            onAddFile(e.target.files[0])
        }
    }

    const onTextFieldBlur = (e: React.FocusEvent<HTMLInputElement>) => {
        onAddFolder && onAddFolder(e.target.value)
        setIsAddFolderMode(false)
    }
    return (<Card elevation={1}>
        {
            isAddFolderMode ?
                <TextField onBlur={onTextFieldBlur} />
                :
                <>
                    <IconButton onClick={() => setIsAddFolderMode(true)}>
                        <CreateNewFolder />
                    </IconButton>
                    <input id="icon-button-file" type="file" hidden onChange={handleInputChange} />
                    <label htmlFor="icon-button-file">
                        <IconButton component="span">
                            <AttachFile />
                        </IconButton>
                    </label>
                </>
        }
    </Card>)
}

export default AddTreeEntity