import { IconButton, TextField, Typography } from '@material-ui/core'
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
    return (<div>
        {
            isAddFolderMode ?
                <TextField onBlur={onTextFieldBlur} />
                :
                <>
                    <Typography variant={"subtitle2"}>
                        {"Create a folder/Upload a file:"}
                    </Typography>
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
    </div>)
}

export default AddTreeEntity