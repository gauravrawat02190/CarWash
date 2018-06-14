import React,{Component} from 'react';


class EditRequest extends Component
{
    constructor(props)
    {
        super(props);
        this.ShowEditRequest= this.ShowEditRequest.bind(this);
    }

    ShowEditRequest(prop,e)
    {
        this.props.ShowEditRequest({Time:prop.Time,ShowRequest:true,ActiveId:prop.id});
        e.preventDefault();
    }

    render()
    {
        if(!this.props.Show.IsVacant)
        {
            return( <a href='#' onClick= {(e) => this.ShowEditRequest(this.props.Show, e)}>Request</a>);
        }
        return (<p></p>);
    }

}

export default EditRequest;
