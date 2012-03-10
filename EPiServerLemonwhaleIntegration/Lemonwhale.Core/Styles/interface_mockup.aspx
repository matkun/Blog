<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Media selector</title>
    <link href="MediaSelector.css" type="text/css" rel="stylesheet" media="all" />
</head>
<body>
    
    <div id="mediaSelector_ScriptSafeClientID" class="media-selector-container">
        <h1>Lemonwhale media selector</h1>
        <fieldset>
            <legend>Filter</legend>
            
            <div class="section">
                <div class="input-container">
                    <label for="title">Title search:</label>
                    <input type="text" id="title" name="title" />
                </div>

                <div class="input-container">
                    <label for="sort-by">Sort by:</label>
                    <select id="sort-by" name="sort-by">
                        <option value="-">-</option>
                        <option value="11111111-1111-1111-1111-111111111111">Title</option>
                        <option value="11111111-1111-1111-1111-111111111111">Created</option>
                        <option value="11111111-1111-1111-1111-111111111111">Published</option>
                        <option value="11111111-1111-1111-1111-111111111111">Number of views</option>
                        <option value="11111111-1111-1111-1111-111111111111">Duration</option>
                    </select>
                </div>
            
                <div class="input-container">
                    <label for="sort-order">Sort order:</label>
                    <select id="sort-order" name="sort-order">
                        <option value="-">-</option>
                        <option value="asc">Ascending</option>
                        <option value="desc">Descending</option>
                    </select>
                </div>
            
                <div class="input-container">
                    <label for="from-date">From date:</label>
                    <input type="text" id="from-date" name="from-date" />
                </div>
            
                <div class="input-container">
                    <label for="to-date">To date:</label>
                    <input type="text" id="to-date" name="to-date" />
                </div>
            </div>
            <div class="section categories-section">
                <div class="input-container">
                    <label for="categories">Categories:</label>
                    <select class="multiselect-categories" id="categories" name="categories" size="7" multiple="">
                        <option>Category one</option>
                        <option>Category two</option>
                        <option>Category three</option>
                        <option>Category four</option>
                        <option>Category five</option>
                        <option>Category six</option>
                        <option>Category seven</option>
                        <option>Category eight</option>
                        <option>Category nine</option>
                        <option>Category ten</option>
                    </select>
                </div>
            </div>
            <div class="buttons">
                <span class="button-container cancel"><input class="cancel-button" type="submit" value="Cancel" /></span>
            </div>
        </fieldset>
        
        <div id="Result">
            <ul>
                <li>
                    <img class="thumbnail" title="image sample" alt="image sample" src="http://pae1d8a01.lwcdn.com/v-i-02c26624-4c68-4b48-90f7-4d4cb6722d7f-0124x70.jpg"/>
                    <div class="info">
                        <div><strong>Title: </strong>Sample movie (<strong>Last modified: </strong>2011-03-14)</div>
                        <div><strong>Description: </strong>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas aliquam, tortor eu imperdiet condimentum, tortor sapien laoreet mauris, rhoncus aliquet urna lectus sit amet quam. Phasellus vestibulum, quam et adipiscing porta, massa ipsum porta dolor, quis convallis sapien diam quis nisl. Cras et vulputate mi. Cras ac scelerisque ipsum. Curabitur consectetur ultrices tellus, vitae elementum enim scelerisque a. Mauris consectetur posuere magna, et elementum nisl malesuada ac. Aenean metus leo, lacinia nec lacinia eget, porttitor ut orci. Phasellus eu nibh nibh. Phasellus ut odio mi. Vivamus sed libero neque.</div>
                    </div>
                    <span class="button-container"><input class="select-button" type="submit" value="" /></span>
                </li>
                <li>
                    <img class="thumbnail" title="image sample" alt="image sample" src="http://pae1d8a01.lwcdn.com/v-i-02c26624-4c68-4b48-90f7-4d4cb6722d7f-0124x70.jpg"/>
                    <div class="info">
                        <div><strong>Title: </strong>Sample movie (<strong>Last modified: </strong>2011-03-14)</div>
                        <div><strong>Description: </strong>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas aliquam, tortor eu imperdiet condimentum, tortor sapien laoreet mauris, rhoncus aliquet urna lectus sit amet quam. Phasellus vestibulum, quam et adipiscing porta, massa ipsum porta dolor, quis convallis sapien diam quis nisl. Cras et vulputate mi. Cras ac scelerisque ipsum. Curabitur consectetur ultrices tellus, vitae elementum enim scelerisque a. Mauris consectetur posuere magna, et elementum nisl malesuada ac. Aenean metus leo, lacinia nec lacinia eget, porttitor ut orci. Phasellus eu nibh nibh. Phasellus ut odio mi. Vivamus sed libero neque.</div>
                    </div>
                    <span class="button-container"><input class="select-button" type="submit" value="" /></span>
                </li>
                <li>
                    <img class="thumbnail" title="image sample" alt="image sample" src="http://pae1d8a01.lwcdn.com/v-i-02c26624-4c68-4b48-90f7-4d4cb6722d7f-0124x70.jpg"/>
                    <div class="info">
                        <div><strong>Title: </strong>Sample movie (<strong>Last modified: </strong>2011-03-14)</div>
                        <div><strong>Description: </strong>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas aliquam, tortor eu imperdiet condimentum, tortor sapien laoreet mauris, rhoncus aliquet urna lectus sit amet quam. Phasellus vestibulum, quam et adipiscing porta, massa ipsum porta dolor, quis convallis sapien diam quis nisl. Cras et vulputate mi. Cras ac scelerisque ipsum. Curabitur consectetur ultrices tellus, vitae elementum enim scelerisque a. Mauris consectetur posuere magna, et elementum nisl malesuada ac. Aenean metus leo, lacinia nec lacinia eget, porttitor ut orci. Phasellus eu nibh nibh. Phasellus ut odio mi. Vivamus sed libero neque.</div>
                    </div>
                    <span class="button-container"><input class="select-button" type="submit" value="" /></span>
                </li>
            </ul>
        </div>
    </div>

</body>
</html>
