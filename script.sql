CREATE TABLE IF NOT EXISTS public.event
(
    id integer NOT NULL DEFAULT nextval('event_id_seq'::regclass),
    description character varying COLLATE pg_catalog."default",
    is_active boolean,
    date_start timestamp without time zone,
    expire_at timestamp without time zone,
    last_modified_by character varying COLLATE pg_catalog."default",
    last_modified timestamp without time zone,
    created timestamp without time zone,
    created_by character varying COLLATE pg_catalog."default",
    CONSTRAINT event_pkey PRIMARY KEY (id)
)


CREATE TABLE IF NOT EXISTS public.event_user
(
    id integer NOT NULL DEFAULT nextval('event_user_id_seq'::regclass),
    fk_event integer,
    fk_user_id character varying COLLATE pg_catalog."default",
    created_by character varying COLLATE pg_catalog."default",
    created timestamp without time zone,
    last_modified_by character varying COLLATE pg_catalog."default",
    last_modified timestamp without time zone,
    CONSTRAINT event_user_pkey PRIMARY KEY (id)
)

CREATE TABLE IF NOT EXISTS public.quiz
(
    id integer NOT NULL DEFAULT nextval('quiz_quiz_seq'::regclass),
    hint character varying COLLATE pg_catalog."default",
    question character varying COLLATE pg_catalog."default",
    answer character varying COLLATE pg_catalog."default",
    fk_event integer,
    has_next_question boolean,
    title character varying COLLATE pg_catalog."default",
    created_by character varying COLLATE pg_catalog."default",
    created timestamp without time zone,
    last_modified_by character varying COLLATE pg_catalog."default",
    last_modified timestamp without time zone,
    CONSTRAINT quiz_pkey PRIMARY KEY (id),
    CONSTRAINT quiz_fk_event_fkey FOREIGN KEY (fk_event)
        REFERENCES public.event (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

CREATE TABLE IF NOT EXISTS public.reward
(
    id integer NOT NULL DEFAULT nextval('reward_id_seq'::regclass),
    coins integer,
    expirience integer,
    claimed boolean,
    role character varying COLLATE pg_catalog."default",
    participant_reward boolean,
    fk_event integer,
    created_by character varying COLLATE pg_catalog."default",
    created timestamp without time zone,
    last_modified_by character varying COLLATE pg_catalog."default",
    last_modified timestamp without time zone,
    CONSTRAINT reward_pkey PRIMARY KEY (id),
    CONSTRAINT fk_event FOREIGN KEY (fk_event)
        REFERENCES public.event (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)
