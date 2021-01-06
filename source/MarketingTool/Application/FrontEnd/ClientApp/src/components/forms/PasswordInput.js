import React from 'react';

export default function PasswordInput(props) {
    return (
        <div>
            {props.labelText ? <label style={props.labelStyle} className={props.labelClassName}>{props.labelText}</label> : null}
            <input type='password' style={props.style} name={props.name} value={props.value} id={props.id} placeholder={props.placeholder} onChange={props.onChange} className={props.className}>{props.display}</input>
            <span style={props.validationStyle} className={`text-danger ${props.validationClassName}`}>{props.validationText}</span>
        </div>
    )
}